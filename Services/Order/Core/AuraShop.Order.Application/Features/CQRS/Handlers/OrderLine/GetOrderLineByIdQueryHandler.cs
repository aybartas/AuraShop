using AuraShop.Order.Application.Features.CQRS.Commands.OrderLine;
using AuraShop.Order.Application.Features.CQRS.Queries.OrderLine;
using AuraShop.Order.Application.Features.CQRS.Results.OrderLine;
using AuraShop.Order.Application.Interfaces;

namespace AuraShop.Order.Application.Features.CQRS.Handlers.OrderLine;

public class GetOrderLineByIdQueryHandler
{
    private readonly IRepository<Domain.Entities.OrderLine> _repository;
    public GetOrderLineByIdQueryHandler(IRepository<Domain.Entities.OrderLine> orderLineRepository)
    {
        _repository = orderLineRepository;
    }
    public async Task<GetOrderLineByIdQueryResult> Handle(GetOrderLineByIdQuery query)
    {
        var orderLine = await _repository.GetByIdAsync(query.Id);

        var result = new GetOrderLineByIdQueryResult
        {
            Id = orderLine.Id,
            ProductId = orderLine.ProductId,
            ProductName = orderLine.ProductName,
            ProductPrice = orderLine.ProductPrice,
            ProductTotalPrice = orderLine.ProductTotalPrice,
            OrderId = orderLine.OrderId,
        };

        return result;
    }
}
