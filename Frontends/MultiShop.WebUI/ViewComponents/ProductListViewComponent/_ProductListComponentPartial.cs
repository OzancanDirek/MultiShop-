using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using MultiShop.DtoLayer.CatalogDtos.ProductDtos;
namespace MultiShop.WebUI.ViewComponents.ProductListViewComponent
{
    public class _ProductListComponentPartial : ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public _ProductListComponentPartial(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync(string id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync($"https://localhost:7071/api/Product/ProductListWithCategoryByCategoryId?id={id}");

            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();

                var values = JsonConvert.DeserializeObject<List<ResultProductWithCategoryDto>>(jsonData)
                             ?? new List<ResultProductWithCategoryDto>();

                return View(values);
            }

            return View(new List<ResultProductWithCategoryDto>());
        }
    }
}
