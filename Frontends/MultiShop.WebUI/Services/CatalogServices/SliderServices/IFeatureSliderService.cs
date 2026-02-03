using MultiShop.DtoLayer.CatalogDtos.FeatureSliderDto;

namespace MultiShop.WebUI.Services.CatalogServices.SliderServices
{
    public interface IFeatureSliderService
    {
        Task<List<ResultFeatureSliderDto>> GetAllFeatureAsync();
        Task CreateFeatureAsync(CreateFeatureSliderDto createFeatureDto);
        Task UpdateFeatureAsync(UpdateFeatureSliderDto updateFeatureDto);
        Task<UpdateFeatureSliderDto> GetByIdFeatureAsync(string FeatureId);
        Task DeleteFeatureAsync(string FeatureId);
        Task FeatureSliderChangeStatusToTrue(string id);
        Task FeatureSliderChangeStatusToFalse(string id);
    }
}
