using MultiShop.DtoLayer.CatalogDtos.FeatureSliderDto;
using Newtonsoft.Json;

namespace MultiShop.WebUI.Services.CatalogServices.SliderServices
{
    public class FeatureSliderService : IFeatureSliderService
    {
        private readonly HttpClient _httpClient;
        public FeatureSliderService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task CreateFeatureAsync(CreateFeatureSliderDto createFeatureDto)
        {
            await _httpClient.PostAsJsonAsync<CreateFeatureSliderDto>("featureslider", createFeatureDto);
        }

        public async Task DeleteFeatureAsync(string FeatureId)
        {
            await _httpClient.DeleteAsync("featureslider?id=" + FeatureId);
        }

        public Task FeatureSliderChangeStatusToFalse(string id)
        {
            throw new NotImplementedException();
        }

        public Task FeatureSliderChangeStatusToTrue(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<ResultFeatureSliderDto>> GetAllFeatureAsync()
        {
            var responseMessage = await _httpClient.GetAsync("featureslider");
            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<List<ResultFeatureSliderDto>>(jsonData);
            return values;
        }

        public async Task<UpdateFeatureSliderDto> GetByIdFeatureAsync(string FeatureId)
        {
            var responseMessage = await _httpClient.GetAsync("featureslider/" + FeatureId);
            var values = await responseMessage.Content.ReadFromJsonAsync<UpdateFeatureSliderDto>();
            return values;
        }

        public async Task UpdateFeatureAsync(UpdateFeatureSliderDto updateFeatureDto)
        {
            await _httpClient.PutAsJsonAsync<UpdateFeatureSliderDto>("featureslider", updateFeatureDto);
        }
    }
}
