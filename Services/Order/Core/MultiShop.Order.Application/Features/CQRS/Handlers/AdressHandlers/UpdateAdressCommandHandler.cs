using MultiShop.Order.Application.Features.CQRS.Commands.AdressCommands;
using MultiShop.Order.Application.Interfaces;
using MultiShop.Order.Domain.Entities;

namespace MultiShop.Order.Application.Features.CQRS.Handlers.AdressHandlers
{
    public class UpdateAdressCommandHandler
    {
        private readonly IRepository<Adress> _repository;
        public UpdateAdressCommandHandler(IRepository<Adress> repository)
        {
            _repository = repository;
        }
        public async Task Handle(UpdateAdressCommand command)
        {
            var adress = await _repository.GetByIdAsync(command.AdressId);
            adress.City = command.City;
            adress.Detail = command.Detail;
            adress.District = command.District;
            adress.UserId = command.UserId;
            await _repository.UpdateAsync(adress);
        }
    }
}
