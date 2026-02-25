using MultiShop.DtoLayer.CargoDtos.CargoCustomerDto;

namespace MultiShop.WebUI.Services.CargoServices.CargoCustomerServices
{
    public interface ICargoCustomerService
    {
        Task<GetCargoCustomerByIdDto> GetByIdCargoCustomerAsync(string id);
    }
}
