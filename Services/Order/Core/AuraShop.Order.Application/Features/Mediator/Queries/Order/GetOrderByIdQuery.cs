using AuraShop.Order.Application.Features.Mediator.Results.Order;
using MediatR;

namespace AuraShop.Order.Application.Features.Mediator.Queries.Order;

public class GetOrderByIdQuery : IRequest<GetOrderByIdQueryResult>
{
    public int Id { get; set; }
}