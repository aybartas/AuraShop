using AuraShop.Order.Application.Features.Mediator.Results.Order;
using MediatR;

namespace AuraShop.Order.Application.Features.Mediator.Queries.Order
{
    public class GetOrderQuery : IRequest<List<GetOrderQueryResult>>
    {
    }
}
