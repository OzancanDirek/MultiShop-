using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MultiShop.DtoLayer.CatalogDtos.FeatureDtos;
using Newtonsoft.Json;

namespace MultiShop.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [AllowAnonymous]
    [Route("Admin/Feature")]
    public class FeatureController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public FeatureController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [Route("Index")]
        public async Task<IActionResult> Index()
        {
            ViewBag.v1 = "Ana Sayfa";
            ViewBag.v2 = "Öne Çıkarılanlar";
            ViewBag.v3 = "Öne Çıkarılan Listesi";
            ViewBag.v0 = "Öne Çıkarılan İşlemleri";

            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:7071/api/Feature");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultFeatureDto>>(jsonData);
                return View(values);
            }

            return View();
        }

        [HttpGet]
        [Route("CreateFeature")]

        public IActionResult CreateFeature()
        {
            ViewBag.v1 = "Ana Sayfa";
            ViewBag.v2 = "Öne Çıkarılanlar";
            ViewBag.v3 = "Öne Çıkarılan Listesi";
            ViewBag.v0 = "Öne Çıkarılan İşlemleri";
            return View();
        }

        [Route("CreateFeature")]
        [HttpPost]
        public async Task<IActionResult> CreateFeature(CreateFeatureDto createFeatureDto)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(createFeatureDto);
            StringContent stringContent = new StringContent(jsonData, System.Text.Encoding.UTF8, "application/json");
            var responseMessage = await client.PostAsync("https://localhost:7071/api/Feature", stringContent);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Feature", new { area = "Admin" });
            }
            return View();
        }

        [Route("DeleteFeature/{id}")]
        public async Task<IActionResult> DeleteFeature(string id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.DeleteAsync("https://localhost:7071/api/Feature?id=" + id);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Feature", new { area = "Admin" });
            }
            return View();
        }

        [Route("UpdateFeature/{id}")]
        [HttpGet]
        public async Task<IActionResult> UpdateFeature(string id)
        {
            ViewBag.v1 = "Ana Sayfa";
            ViewBag.v2 = "Öne Çıkarılanlar";
            ViewBag.v3 = "Öne Çıkarılan Listesi";
            ViewBag.v0 = "Öne Çıkarılan İşlemleri";
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:7071/api/Feature/" + id);
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<UpdateFeatureDto>(jsonData);
                return View(values);
            }
            return View();
        }

        [Route("UpdateFeature/{id}")]
        [HttpPost]
        public async Task<IActionResult> UpdateFeature(UpdateFeatureDto updateFeatureDto)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(updateFeatureDto);
            StringContent stringContent = new StringContent(jsonData, System.Text.Encoding.UTF8, "application/json");
            var responseMessage = await client.PutAsync("https://localhost:7071/api/Feature/", stringContent);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Feature", new { area = "Admin" });
            }
            return View();
        }
    }
}
