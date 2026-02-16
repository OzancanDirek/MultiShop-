using MultiShop.Order.Application.Features.CQRS.Commands.AdressCommands;
using MultiShop.Order.Application.Interfaces;
using MultiShop.Order.Domain.Entities;

namespace MultiShop.Order.Application.Features.CQRS.Handlers.AdressHandlers
{
    public class CreateAdressCommandHandler // Note: Bütün adresleri olusturmak için kullanılılan handler
    {
        private readonly IRepository<Adress> _repository;

        public CreateAdressCommandHandler(IRepository<Adress> repository)
        {
            _repository = repository;
        }
        public async Task Handle(CreateAdressCommand createAdressCommand)
        {
            await _repository.CreateAsync(new Adress
            {
                City = createAdressCommand.City,
                District = createAdressCommand.District,
                Detail1 = createAdressCommand.Detail1,
                UserId = createAdressCommand.UserId,
                Country = createAdressCommand.Country,
                Description = createAdressCommand.Description,
                Detail2 = createAdressCommand.Detail2,
                Email = createAdressCommand.Email,
                Name = createAdressCommand.Name,
                Surname = createAdressCommand.Surname,
                Phone = createAdressCommand.Phone,
                ZipCode = createAdressCommand.ZipCode
            });
        }
    }
}
