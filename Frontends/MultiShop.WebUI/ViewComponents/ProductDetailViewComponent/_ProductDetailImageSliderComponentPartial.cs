using Microsoft.AspNetCore.Mvc;
using MultiShop.DtoLayer.CatalogDtos.ProductImageDtos;
using Newtonsoft.Json;

namespace MultiShop.WebUI.ViewComponents.ProductDetailViewComponent
{
    public class _ProductDetailImageSliderComponentPartial : ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public _ProductDetailImageSliderComponentPartial(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<IViewComponentResult> InvokeAsync(string productId)
        {
            if (string.IsNullOrEmpty(productId))
                return View(null);

            var client = _httpClientFactory.CreateClient();

            var responseMessage = await client.GetAsync( $"https://localhost:7071/api/ProductImage/ProductImagesByProductId/{productId}");

            if (responseMessage.StatusCode == System.Net.HttpStatusCode.NoContent)
                return View(null);

            if (!responseMessage.IsSuccessStatusCode)
                return View(null);

            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<GetByIdProductImageDto>(jsonData);

            return View(values);
        }

    }
}
