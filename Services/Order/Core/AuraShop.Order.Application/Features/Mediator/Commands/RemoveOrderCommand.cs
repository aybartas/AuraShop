using MediatR;

namespace AuraShop.Order.Application.Features.Mediator.Commands;

public class RemoveOrderCommand : IRequest
{
    public int Id { get; set; }
}