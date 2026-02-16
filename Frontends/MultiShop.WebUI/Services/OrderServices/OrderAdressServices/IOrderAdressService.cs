using MultiShop.DtoLayer.OrderDtos.OrderAdressDtos;

namespace MultiShop.WebUI.Services.OrderServices.OrderAdressServices
{
    public interface IOrderAdressService
    {
        //Task<List<ResultAboutDto>> GetAllAboutAsync();
        Task CreateOrderAdressAsync(CreateOrderAdressDto createAboutDto);
        //Task UpdateAboutAsync(UpdateAboutDto updateAboutDto);
        //Task<UpdateAboutDto> GetByIdAboutAsync(string AboutId);
        //Task DeleteAboutAsync(string AboutId);
    }
}
