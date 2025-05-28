using MediatR;
using AuraShop.Order.Application.Features.Mediator.Commands;
using AuraShop.Order.Application.Interfaces;

namespace AuraShop.Order.Application.Features.Mediator.Handlers.Order
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand>
    {
        private readonly IRepository<Domain.Entities.Order> _repository;

        public CreateOrderCommandHandler(IRepository<Domain.Entities.Order> repository)
        {
            _repository = repository;
        }

        public async Task Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var order = new Domain.Entities.Order
            {
                UserId = request.UserId,
                TotalPrice = request.TotalPrice,
                OrderDate = DateTime.UtcNow
            };

            await _repository.CreateAsync(order);
        }
    }
}
