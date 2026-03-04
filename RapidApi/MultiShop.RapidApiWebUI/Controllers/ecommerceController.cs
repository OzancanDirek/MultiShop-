using Microsoft.AspNetCore.Mvc;
using MultiShop.RapidApiWebUI.Models;
using Newtonsoft.Json;

namespace MultiShop.RapidApiWebUI.Controllers
{
    public class ecommerceController : Controller
    {
        public async Task<IActionResult> ECommerceList()
        {
            var client = new HttpClient();

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://real-time-product-search.p.rapidapi.com/deals-v2?q=Logitech%20fare&country=us&language=tr&page=1&limit=10&sort_by=BEST_MATCH&product_condition=ANY"),
                Headers =
        {
            { "x-rapidapi-key", "6735838d46msh45f1749218a51bfp191bd6jsne91873cc97e0" },
            { "x-rapidapi-host", "real-time-product-search.p.rapidapi.com" }
        }
            };

            var response = await client.SendAsync(request);

            response.EnsureSuccessStatusCode();

            var body = await response.Content.ReadAsStringAsync();

            var root = JsonConvert.DeserializeObject<Rootobject>(body);

            var list = root?.data?.products?
             .Select(x => new eCommerceViewModel
             {
                 Title = x?.product_title,
                 Price = x?.offer?.price,
                 OriginalPrice = x?.offer?.original_price,
                 Rating = x?.product_rating ?? 0,
                 ProductUrl = x?.product_page_url,
                 ImageUrl = x?.product_photos?.FirstOrDefault()
             }).ToList() ?? new List<eCommerceViewModel>();

            return View(list);
        }
    }
}