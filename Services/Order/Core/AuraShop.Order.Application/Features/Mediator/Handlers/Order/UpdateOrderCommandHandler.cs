using AuraShop.Order.Application.Features.Mediator.Commands;
using AuraShop.Order.Application.Interfaces;
using MediatR;

namespace AuraShop.Order.Application.Features.Mediator.Handlers.Order;

public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand>
{
    private readonly IRepository<Domain.Entities.Order> _repository;

    public UpdateOrderCommandHandler(IRepository<Domain.Entities.Order> repository)
    {
        _repository = repository;
    }

    public async Task Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
    {
        var existingOrder = await _repository.GetByIdAsync(request.Id);

        existingOrder.TotalPrice = request.TotalPrice;
        existingOrder.UserId = request.UserId;

        await _repository.UpdateAsync(existingOrder);
    }
}