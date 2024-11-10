using AuraShop.Order.Application.Interfaces;
using AuraShop.Order.Application.Features.CQRS.Commands.OrderLine;

namespace AuraShop.Order.Application.Features.CQRS.Handlers.OrderLine
{
    public class RemoveOrderLineCommandHandler
    {
        private readonly IRepository<Domain.Entities.OrderLine> _repository;

        public RemoveOrderLineCommandHandler(IRepository<Domain.Entities.OrderLine> repository)
        {
            _repository = repository;
        }

        public async Task Handle(RemoveOrderLineCommand command)
        {
            var value = await _repository.GetByIdAsync(command.Id);
            await _repository.DeleteAsync(value);
        }
    }
}
