using MediatR;
using MultiShop.Order.Application.Features.Mediator.Results.OrderingResult;

public class GetOrderingByUserIdQuery: IRequest<List<GetOrderingByUserIdQueryResult>>
{
    public string Id { get; set; }

    public GetOrderingByUserIdQuery(string id)
    {
        Id = id;
    }
}
