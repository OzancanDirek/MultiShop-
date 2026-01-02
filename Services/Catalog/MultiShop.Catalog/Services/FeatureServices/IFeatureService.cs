using MultiShop.Catalog.Dtos.FeatureDtos;

namespace MultiShop.Catalog.Services.FeatureServices
{
    public interface IFeatureService
    {
        Task<List<ResultFeatureDto>> GetAllFeaturesAsync();
        Task CreateFeatureAsync(CreateFeatureDto createFeatureDto);
        Task UpdateFeatureAsync(UpdateFeatureDto updateFeatureDto);
        Task<GetByIdFeatureDto> GetByIdFeatureAsync(string FeatureId);
        Task DeleteFeatureAsync(string FeatureId);
    }
}
