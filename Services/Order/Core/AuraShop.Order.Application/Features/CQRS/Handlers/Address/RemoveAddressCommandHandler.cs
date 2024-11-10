using AuraShop.Order.Application.Features.CQRS.Commands.Address;
using AuraShop.Order.Application.Interfaces;

namespace AuraShop.Order.Application.Features.CQRS.Handlers.Address;

public class RemoveAddressCommandHandler
{
    private readonly IRepository<Domain.Entities.Address> _repository;

    public RemoveAddressCommandHandler(IRepository<Domain.Entities.Address> repository)
    {
        _repository = repository;
    }

    public async Task Handle(RemoveAddressCommand command)
    {
        var value = await _repository.GetByIdAsync(command.Id);
        await _repository.DeleteAsync(value);
    }
}