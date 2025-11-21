using MultiShop.Order.Application.Features.CQRS.Results.AdressQueries;
using MultiShop.Order.Application.Interfaces;
using MultiShop.Order.Domain.Entities;

namespace MultiShop.Order.Application.Features.CQRS.Handlers.AdressHandlers
{
    public class GetAdressQueryHandler // Note: Bütün adresleri getirmek için kullanılılan handler
    {
        private readonly IRepository<Adress> _repository;

        public GetAdressQueryHandler(IRepository<Adress> repository)
        {
            _repository = repository;
        }

        public async Task<List<GetAdressQueryResult>> Handle()
        {
            var values = await _repository.GetAllAsync();
            return values.Select(x => new GetAdressQueryResult
            {
                AdressId = x.AdressId,
                UserId = x.UserId,
                District = x.District,
                City = x.City,
                Detail = x.Detail
            }).ToList();
        }
    }
}
