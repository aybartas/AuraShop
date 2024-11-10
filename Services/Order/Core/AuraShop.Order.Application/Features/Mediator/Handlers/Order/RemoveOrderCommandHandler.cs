using MediatR;
using AuraShop.Order.Application.Features.Mediator.Commands;
using AuraShop.Order.Application.Interfaces;

namespace AuraShop.Order.Application.Features.Mediator.Handlers.Order
{
    public class RemoveOrderCommandHandler : IRequestHandler<RemoveOrderCommand>
    {
        private readonly IRepository<Domain.Entities.Order> _repository;

        public RemoveOrderCommandHandler(IRepository<Domain.Entities.Order> repository)
        {
            _repository = repository;
        }

        public async Task Handle(RemoveOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await _repository.GetByIdAsync(request.Id);
            await _repository.DeleteAsync(order);
        }
    }
}
