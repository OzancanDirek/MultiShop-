namespace MultiShop.Order.Application.Features.CQRS.Queries.AdressQueries
{
    public class GetAdressByIdQuery
    {
        public int Id { get; set; }

        public GetAdressByIdQuery(int id)
        {
            Id = id;
        }
    }
}
