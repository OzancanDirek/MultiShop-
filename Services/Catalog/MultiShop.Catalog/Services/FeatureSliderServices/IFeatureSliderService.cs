using MultiShop.Catalog.Dtos.FeatureSliderDtos;

namespace MultiShop.Catalog.Services.FeatureSliderServices
{
    public interface IFeatureSliderService
    {
        Task<List<ResultFeatureSliderDto>> GetAllFeatureAsync();
        Task CreateFeatureAsync(CreateFeatureSliderDto createFeatureDto);
        Task UpdateFeatureAsync(UpdateFeatureSliderDto updateFeatureDto);
        Task<GetByIdFeatureSliderDto> GetByIdFeatureAsync(string FeatureId);
        Task DeleteFeatureAsync(string FeatureId);
        Task FeatureSliderChangeStatusToTrue(string id);
        Task FeatureSliderChangeStatusToFalse(string id);
    }
}
