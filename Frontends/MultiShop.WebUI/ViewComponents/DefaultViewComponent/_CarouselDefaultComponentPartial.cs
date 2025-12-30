using Microsoft.AspNetCore.Mvc;
using MultiShop.DtoLayer.CatalogDtos.FeatureSliderDto;
using Newtonsoft.Json;

namespace MultiShop.WebUI.ViewComponents.DefaultViiewComponent
{
    public class _CarouselDefaultComponentPartial : ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public _CarouselDefaultComponentPartial(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {

            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:7071/api/FeatureSlider");
            var statusCode = responseMessage.StatusCode;
            var content = await responseMessage.Content.ReadAsStringAsync();
            ViewBag.ApiStatus = $"Status: {statusCode}";
            ViewBag.ApiResponse = content;
            if (responseMessage.IsSuccessStatusCode)
            {
                var values = JsonConvert.DeserializeObject<List<ResultFeatureSliderDto>>(content);
                return View(values ?? new List<ResultFeatureSliderDto>());
            }
            return View(new List<ResultFeatureSliderDto>());
        }
    }
}
