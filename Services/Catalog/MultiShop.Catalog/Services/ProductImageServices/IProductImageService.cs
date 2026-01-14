using MultiShop.Catalog.Dtos.ProductImageDtos;

namespace MultiShop.Catalog.Services.ProductImageServices
{
    public interface IProductImageService
    {
        Task<List<ResultProductImageDto>> GetAllProductImageAsync();
        Task CreateProductImageAsync(CreateProductImageDto createProductImageDto);
        Task UpdateProductImageAsync(UpdateProductImageDto updateProductImageDto);
        Task<GetByIdProductImageDto> GetByIdProductImageAsync(string ProductImageId);
        Task DeleteProductImageAsync(string ProductImageId);
        Task<GetByIdProductImageDto> GetByProductIdProductImageAsync(string id);
    }
}
