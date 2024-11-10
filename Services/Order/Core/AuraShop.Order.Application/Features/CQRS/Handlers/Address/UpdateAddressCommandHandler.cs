using AuraShop.Order.Application.Features.CQRS.Commands.Address;
using AuraShop.Order.Application.Interfaces;

namespace AuraShop.Order.Application.Features.CQRS.Handlers.Address;

public class UpdateAddressCommandHandler
{
    private readonly IRepository<Domain.Entities.Address> _repository;

    public UpdateAddressCommandHandler(IRepository<Domain.Entities.Address> repository)
    {
        _repository = repository;
    }

    public async Task Handle(UpdateAddressCommand command)
    {
        var value = await _repository.GetByIdAsync(command.Id);

        value.City = command.City;
        value.Detail = command.Detail;
        value.UserId = command.UserId;
        value.District = command.District;
        await _repository.UpdateAsync(value);
    }
}