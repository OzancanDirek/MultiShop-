using MultiShop.Order.Application.Features.CQRS.Queries.AdressQueries;
using MultiShop.Order.Application.Features.CQRS.Results.AdressQueries;
using MultiShop.Order.Application.Interfaces;
using MultiShop.Order.Domain.Entities;

namespace MultiShop.Order.Application.Features.CQRS.Handlers.AdressHandlers
{
    public class GetAdressByIdQueryHandler // Note: IDsi verilen adresleri getirmek için kullanılılan handler
    {
        private readonly IRepository<Adress> _repository;
        public GetAdressByIdQueryHandler(IRepository<Adress> repository)
        {
            _repository = repository;
        }
        public async Task<GetAdressByIdQueryResult> Handle(GetAdressByIdQuery query)
        {
            var values = await _repository.GetByIdAsync(query.Id);
            return new GetAdressByIdQueryResult
            {
                AdressId = values.AdressId,
                UserId = values.UserId,
                District = values.District,
                City = values.City,
                Detail = values.Detail
            };
        }
    }
}
