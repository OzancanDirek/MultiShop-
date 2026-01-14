using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MultiShop.DtoLayer.CatalogDtos.ProductImageDtos;
using Newtonsoft.Json;
using System.Text;

namespace MultiShop.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [AllowAnonymous]
    [Route("Admin/ProductImage")]
    public class ProductImageController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ProductImageController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [Route("ProductImageDetail/{id}")]
        [HttpGet]
        public async Task<IActionResult> ProductImageDetail(string id)
        {
            ViewBag.v1 = "Ana Sayfa";
            ViewBag.v2 = "Ürünler";
            ViewBag.v3 = "Ürün Görsel Güncelleme Sayfası";
            ViewBag.v0 = "Ürün Görsel İşlemleri";

            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync(
                $"https://localhost:7071/api/ProductImage/ProductImagesByProductId/{id}");

            if (responseMessage.StatusCode == System.Net.HttpStatusCode.NoContent)
            {
                return View(new UpdateProductImageDto
                {
                    ProductId = id,
                    ProductImageId = "",
                    Image1 = "",
                    Image2 = "",
                    Image3 = "",
                    Image4 = ""
                });
            }

            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var settings = new JsonSerializerSettings
                {
                    ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver()
                };
                var values = JsonConvert.DeserializeObject<UpdateProductImageDto>(jsonData, settings);

                if (values != null)
                {
                    return View(values);
                }
            }

            return View(new UpdateProductImageDto { ProductId = id });
        }

        [HttpPost]
        [Route("ProductImageDetail/{id}")]
        public async Task<IActionResult> ProductImageDetail(string id, UpdateProductImageDto updateProductImageDto)
        {
            updateProductImageDto.ProductId = id;

            var client = _httpClientFactory.CreateClient();
            var settings = new JsonSerializerSettings
            {
                ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver()
            };

            HttpResponseMessage responseMessage;

            if (string.IsNullOrEmpty(updateProductImageDto.ProductImageId))
            {
                var createDto = new CreateProductImageDto
                {
                    ProductId = updateProductImageDto.ProductId,
                    Image1 = updateProductImageDto.Image1,
                    Image2 = updateProductImageDto.Image2,
                    Image3 = updateProductImageDto.Image3,
                    Image4 = updateProductImageDto.Image4
                };

                var createJson = JsonConvert.SerializeObject(createDto, settings);
                StringContent createContent = new StringContent(createJson, Encoding.UTF8, "application/json");
                responseMessage = await client.PostAsync("https://localhost:7071/api/ProductImage", createContent);
            }
            else
            {
                var jsonData = JsonConvert.SerializeObject(updateProductImageDto, settings);
                StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
                responseMessage = await client.PutAsync("https://localhost:7071/api/ProductImage", stringContent);
            }

            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("ProductListWithCategory", "Product", new { area = "Admin" });
            }

            return View(updateProductImageDto);
        }
    }
}
