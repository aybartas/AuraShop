using AuraShop.Order.Application.Features.CQRS.Commands.Address;
using AuraShop.Order.Application.Interfaces;

namespace AuraShop.Order.Application.Features.CQRS.Handlers.Address
{
    public class CreateAddressCommandHandler
    {
        private readonly IRepository<Domain.Entities.Address> _repository;

        public CreateAddressCommandHandler(IRepository<Domain.Entities.Address> repository)
        {
            _repository = repository;
        }

        public async Task Handle(CreateAddressCommand command)
        {
            var address = new Domain.Entities.Address
            {
                UserId = command.UserId,
                District = command.District,
                City = command.City,
                Detail = command.Detail
            };

            await _repository.CreateAsync(address);
        }
    }
}
