
using MultiShop.DtoLayer.IdentityDtos.UserDtos;
using Newtonsoft.Json;

namespace MultiShop.WebUI.Services.StatisticServices.UserStatisticServices
{
    public class UserStatisticServices : IUserStatisticServices
    {
        private readonly HttpClient _httpClient;

        public UserStatisticServices(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<int> GetUserCount()
        {
            var responseMessage = await _httpClient.GetAsync("http://localhost:5001/api/Statistics");
            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<int>(jsonData);
            return values;
        }
    }
}
