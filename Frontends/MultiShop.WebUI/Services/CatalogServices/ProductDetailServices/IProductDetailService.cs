using MultiShop.DtoLayer.CatalogDtos.ProductDetailDtos;

namespace MultiShop.WebUI.Services.CatalogServices.ProductDetailServices
{
    public interface IProductDetailService
    {
        Task<List<ResultProductDetailDto>> GetAllProductDetailAsync();
        Task CreateProductDetailAsync(CreateProductDetailDto createProductDetailDto);
        Task UpdateProductDetailAsync(UpdateProductDetailDto updateProductDetailDto);
        Task<GetByIdProductDetailDto> GetByIdProductDetailAsync(string ProductDetailId);
        Task DeleteProductDetailAsync(string ProductDetailId);
        Task<GetByIdProductDetailDto> GetByProductIdProductDetailAsync(string id);
    }
}
