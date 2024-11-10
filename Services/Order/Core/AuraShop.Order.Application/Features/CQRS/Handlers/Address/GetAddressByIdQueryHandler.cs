using AuraShop.Order.Application.Features.CQRS.Commands.Address;
using AuraShop.Order.Application.Features.CQRS.Queries.Address;
using AuraShop.Order.Application.Features.CQRS.Results.Address;
using AuraShop.Order.Application.Interfaces;
using AuraShop.Order.Domain.Entities;

namespace AuraShop.Order.Application.Features.CQRS.Handlers.Address
{
    public class GetAddressByIdQueryHandler
    {
        private readonly IRepository<Domain.Entities.Address> _repository;

        public GetAddressByIdQueryHandler(IRepository<Domain.Entities.Address> repository)
        {
            _repository = repository;
        }

        public async Task<GetAddressByIdQueryResult> Handle(GetAddressByIdQuery query)
        {
            var address = await _repository.GetByIdAsync(query.Id);

            var response = new GetAddressByIdQueryResult
            {
                Id = address.Id,
                UserId = address.UserId,
                District = address.District,
                City = address.City,
                Detail = address.Detail
            };
            return response;

        }
    }
}