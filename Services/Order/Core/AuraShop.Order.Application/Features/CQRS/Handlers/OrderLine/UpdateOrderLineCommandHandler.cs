using AuraShop.Order.Application.Features.CQRS.Commands.OrderLine;
using AuraShop.Order.Application.Interfaces;

namespace AuraShop.Order.Application.Features.CQRS.Handlers.OrderLine;

public class UpdateOrderLineCommandHandler
{
    private readonly IRepository<Domain.Entities.OrderLine> _repository;
    public UpdateOrderLineCommandHandler(IRepository<Domain.Entities.OrderLine> orderLineRepository)
    {
        _repository = orderLineRepository;
    }
    public async Task Handle(UpdateOrderLineCommand command)
    {
        var orderLine = new Domain.Entities.OrderLine
        {
            Id = command.Id,
            ProductId = command.ProductId,
            ProductName = command.ProductName,
            ProductPrice = command.ProductPrice,
            ProductTotalPrice = command.ProductTotalPrice,
            OrderId = command.OrderId
        };

        await _repository.UpdateAsync(orderLine);
    }
}

