using MultiShop.DtoLayer.BasketDtos;
using System.Text.Json;

namespace MultiShop.WebUI.Services.BasketServices
{
    public class BasketService : IBasketService
    {
        private readonly HttpClient _httpClient;
        public BasketService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task AddBasketItem(BasketItemDto basketItemDto)
        {
            var values = await GetBasket();
            if (values != null)
            {
                if (!values.BasketItems.Any(x => x.ProductId == basketItemDto.ProductId))
                {
                    values.BasketItems.Add(basketItemDto);
                }
                else
                {
                    var existingItem = values.BasketItems.First(x => x.ProductId == basketItemDto.ProductId);
                    existingItem.Quantity += basketItemDto.Quantity;
                }
            }
            await SaveBasket(values);
        }

        public Task DeleteBasket(string userId)
        {
            throw new NotImplementedException();
        }

        public async Task<BasketTotalDto> GetBasket()
        {
            var responseMessage = await _httpClient.GetAsync("baskets");

            if (!responseMessage.IsSuccessStatusCode)
            {
                return new BasketTotalDto { BasketItems = new List<BasketItemDto>() };
            }

            var jsonString = await responseMessage.Content.ReadAsStringAsync();

            if (string.IsNullOrWhiteSpace(jsonString))
            {
                // Eğer cevap boşsa boş basket dön
                return new BasketTotalDto { BasketItems = new List<BasketItemDto>() };
            }

            // Başarılıysa deserialize et
            var values = JsonSerializer.Deserialize<BasketTotalDto>(jsonString, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return values ?? new BasketTotalDto { BasketItems = new List<BasketItemDto>() };
        }

        public async Task<bool> RemoveBasketItem(string productId)
        {
            var values = await GetBasket();
            var deletedItem = values.BasketItems.FirstOrDefault(x => x.ProductId == productId);
            var result = values.BasketItems.Remove(deletedItem);
            await SaveBasket(values);
            return true;
        }

        public async Task SaveBasket(BasketTotalDto basketTotalDto)
        {
            await _httpClient.PostAsJsonAsync<BasketTotalDto>("baskets", basketTotalDto);
        }
    }
}