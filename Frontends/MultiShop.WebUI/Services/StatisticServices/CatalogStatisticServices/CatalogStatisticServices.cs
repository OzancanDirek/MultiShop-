namespace MultiShop.WebUI.Services.StatisticServices.CatalogStatisticServices
{
    public class CatalogStatisticServices : ICatalogStatisticServices
    {
        private readonly HttpClient _httpClient;
        public CatalogStatisticServices(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<long> GetBrandCount()
        {
            var responseMessage = await _httpClient.GetAsync("statistics/GetBrandCount");
            var values = await responseMessage.Content.ReadFromJsonAsync<long>();
            return values;
        }

        public async Task<long> GetCategoryCount()
        {
            var response = await _httpClient.GetAsync("statistics/GetCategoryCount");
            var debug = await response.Content.ReadAsStringAsync();
            Console.WriteLine("API RESPONSE => " + debug);
            Console.WriteLine("STATUS => " + response.StatusCode);

            if (!response.IsSuccessStatusCode)
                return 0;

            if (string.IsNullOrWhiteSpace(debug))
                return 0;

            return long.Parse(debug);
        }
        public async Task<string> GetMaxPriceProductName()
        {
            var responseMessage = await _httpClient.GetAsync("statistics/GetMaxPriceProductName");
            var values = await responseMessage.Content.ReadAsStringAsync();
            return values;
        }

        public async Task<string> GetMinPriceProductName()
        {
            var responseMessage = await _httpClient.GetAsync("statistics/GetMinPriceProductName");
            var values = await responseMessage.Content.ReadAsStringAsync();
            return values;
        }

        public async Task<decimal> GetProductAvgPrice()
        {
            var responseMessage = await _httpClient.GetAsync("statistics/GetProductAvgPrice");

            if (!responseMessage.IsSuccessStatusCode)
            {
                var error = await responseMessage.Content.ReadAsStringAsync();
                throw new Exception($"API Hatası: {error}");
            }

            var values = await responseMessage.Content.ReadFromJsonAsync<decimal>();
            return values;
        }

        public async Task<long> GetProductCount()
        {
            var responseMessage = await _httpClient.GetAsync("statistics/GetProductCount");
            var values = await responseMessage.Content.ReadFromJsonAsync<long>();
            return values;
        }
    }
}
