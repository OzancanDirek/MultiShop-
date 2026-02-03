using MultiShop.DtoLayer.CatalogDtos.FeatureDtos;
using Newtonsoft.Json;

namespace MultiShop.WebUI.Services.CatalogServices.FeatureServices
{
    public class FeatureService : IFeatureService
    {
        private readonly HttpClient _httpClient;
        public FeatureService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task CreateFeatureAsync(CreateFeatureDto createFeatureDto)
        {
            await _httpClient.PostAsJsonAsync<CreateFeatureDto>("Feature", createFeatureDto);
        }

        public async Task DeleteFeatureAsync(string id)
        {
            await _httpClient.DeleteAsync("Feature?id=" + id);
        }

        public async Task<List<ResultFeatureDto>> GetAllFeaturesAsync()
        {
            var responseMessage = await _httpClient.GetAsync("Feature");
            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<List<ResultFeatureDto>>(jsonData);
            return values;
        }

        public async Task<GetByIdFeatureDto> GetByIdFeatureAsync(string FeatureId)
        {
            var responseMessage = await _httpClient.GetAsync("Feature/" + FeatureId);
            var values = await responseMessage.Content.ReadFromJsonAsync<GetByIdFeatureDto>();
            return values;
        }

        public async Task UpdateFeatureAsync(UpdateFeatureDto updateFeatureDto)
        {
            await _httpClient.PutAsJsonAsync<UpdateFeatureDto>("Feature", updateFeatureDto);
        }
    }
}
