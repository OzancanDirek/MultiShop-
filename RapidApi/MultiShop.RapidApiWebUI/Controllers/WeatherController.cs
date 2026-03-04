using Microsoft.AspNetCore.Mvc;
using MultiShop.RapidApiWebUI.Models;
using Newtonsoft.Json;

namespace MultiShop.RapidApiWebUI.Controllers
{
    public class WeatherController : Controller
    {
        public async Task<IActionResult> WeatherDetail()
        {
            var client = new HttpClient();

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://daily-weather3.p.rapidapi.com/current.json?q=Istanbul&key=e69c1ed1328947a5a96152014252605"),
                Headers =
        {
            { "x-rapidapi-key", "6735838d46msh45f1749218a51bfp191bd6jsne91873cc97e0" },
            { "x-rapidapi-host", "daily-weather3.p.rapidapi.com" }
        }
            };

            var response = await client.SendAsync(request);

            var body = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                return Content(body);
            }

            var values = JsonConvert.DeserializeObject<WeatherViewModel>(body);

            return View(values);
        }


        public async Task<IActionResult> Exchange()
        {
            var client = new HttpClient();

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(
                    "https://real-time-finance-data.p.rapidapi.com/currency-exchange-rate?from_symbol=USD&to_symbol=TRY&language=en"),
                Headers =
                {
                    { "x-rapidapi-key", "6735838d46msh45f1749218a51bfp191bd6jsne91873cc97e0" },
                    { "x-rapidapi-host", "real-time-finance-data.p.rapidapi.com" }
                }
            };

            var response = await client.SendAsync(request);
            var body = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
                return Content(body);

            var usdData = JsonConvert.DeserializeObject<ExchangeViewModel>(body);


            var request2 = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(
                    "https://real-time-finance-data.p.rapidapi.com/currency-exchange-rate?from_symbol=EUR&to_symbol=TRY&language=en"),
                Headers =
                {
                    { "x-rapidapi-key", "6735838d46msh45f1749218a51bfp191bd6jsne91873cc97e0" },
                    { "x-rapidapi-host", "real-time-finance-data.p.rapidapi.com" }
                }
            };

            var response2 = await client.SendAsync(request2);
            var body2 = await response2.Content.ReadAsStringAsync();

            if (!response2.IsSuccessStatusCode)
                return Content(body2);

            var eurData = JsonConvert.DeserializeObject<ExchangeViewModel>(body2);

            var model = new Tuple<ExchangeViewModel, ExchangeViewModel>(usdData, eurData);

            return View(model);
        }
    }
}