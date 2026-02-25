using MultiShop.DtoLayer.CargoDtos.CargoCompanyDtos;

namespace MultiShop.WebUI.Services.CargoServices.CargoCompanyServices
{
    public interface ICargoCompanyService
    {
        Task<List<ResultCargoCompanyDto>> GetAllCargoCompanyAsync();
        Task CreateCargoAsync(CreateCargoCompanyDto createCargoDto);
        Task UpdateCargoAsync(UpdateCargoCompanyDto updateCargoDto);
        Task<UpdateCargoCompanyDto> GetByIdCargoAsync(int id);
        Task DeleteCargoAsync(int id);
    }
}
