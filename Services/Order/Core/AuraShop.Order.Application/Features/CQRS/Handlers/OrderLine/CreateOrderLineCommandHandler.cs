using AuraShop.Order.Application.Features.CQRS.Commands.OrderLine;
using AuraShop.Order.Application.Interfaces;

namespace AuraShop.Order.Application.Features.CQRS.Handlers.OrderLine
{
    public class CreateOrderLineCommandHandler
    {
        private readonly IRepository<Domain.Entities.OrderLine> _repository;
        public CreateOrderLineCommandHandler(IRepository<Domain.Entities.OrderLine> orderLineRepository)
        {
            _repository = orderLineRepository;
        }
        public async Task Handle(CreateOrderLineCommand command)
        {
            var orderLine = new Domain.Entities.OrderLine
            {
                ProductId = command.ProductId,
                ProductName = command.ProductName,
                ProductPrice = command.ProductPrice,
                ProductTotalPrice = command.ProductTotalPrice,
                OrderId = command.OrderId
            };

            await _repository.CreateAsync(orderLine);
        }
    }
}
