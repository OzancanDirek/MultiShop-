using MultiShop.DtoLayer.CatalogDtos.ProductDtos;
using Newtonsoft.Json;

namespace MultiShop.WebUI.Services.CatalogServices.ProductServices
{
    public class ProductService : IProductService
    {
        private readonly HttpClient _httpClient;

        public ProductService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<ResultProductDto>> GetAllProductAsync()
        {
            var responseMessage = await _httpClient.GetAsync("Product");

            if (!responseMessage.IsSuccessStatusCode)
                return new List<ResultProductDto>();

            var jsonData = await responseMessage.Content.ReadAsStringAsync();

            if (string.IsNullOrWhiteSpace(jsonData))
                return new List<ResultProductDto>();

            var values = JsonConvert.DeserializeObject<List<ResultProductDto>>(jsonData);
            return values ?? new List<ResultProductDto>();
        }

        public async Task CreateProductAsync(CreateProductDto createProductDto)
        {
            await _httpClient.PostAsJsonAsync("Product", createProductDto);
        }

        public async Task DeleteProductAsync(string productId)
        {
            await _httpClient.DeleteAsync($"Product?id={productId}");
        }

        public async Task<UpdateProductDto> GetByIdProductAsync(string productId)
        {
            var responseMessage = await _httpClient.GetAsync($"Product/{productId}");
            return await responseMessage.Content.ReadFromJsonAsync<UpdateProductDto>();
        }

        public async Task UpdateProductAsync(UpdateProductDto updateProductDto)
        {
            await _httpClient.PutAsJsonAsync("Product", updateProductDto);
        }

        public async Task<List<ResultProductWithCategoryDto>> GetProductsWithCategoryAsync()
        {
            var responseMessage = await _httpClient.GetAsync("Product/ProductListWithCategory");

            if (!responseMessage.IsSuccessStatusCode)
                return new List<ResultProductWithCategoryDto>();

            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<ResultProductWithCategoryDto>>(jsonData)
                   ?? new List<ResultProductWithCategoryDto>();
        }

        public async Task<List<ResultProductWithCategoryDto>> GetProductWithCategoryByCategoryIdAsync(string categoryId)
        {
            var responseMessage = await _httpClient.GetAsync("Product/ProductListWithCategoryByCategoryId/" + categoryId);
            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<List<ResultProductWithCategoryDto>>(jsonData);
            return values;

        }
    }
}
