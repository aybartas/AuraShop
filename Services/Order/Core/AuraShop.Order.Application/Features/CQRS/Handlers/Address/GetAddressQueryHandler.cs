using AuraShop.Order.Application.Features.CQRS.Results.Address;
using AuraShop.Order.Application.Interfaces;
using AuraShop.Order.Domain.Entities;

namespace AuraShop.Order.Application.Features.CQRS.Handlers.Address;

public class GetAddressQueryHandler
{
    private readonly IRepository<Domain.Entities.Address> _repository;

    public GetAddressQueryHandler(IRepository<Domain.Entities.Address> repository)
    {
        _repository = repository;
    }

    public async Task<List<GetAddressQueryResult>> Handle()
    {
        var address = await _repository.GetAllAsync();

        var response = address.Select(address => new GetAddressQueryResult
        {
            Id = address.Id,
            UserId = address.UserId,
            District = address.District,
            City = address.City,
            Detail = address.Detail
        }).ToList();

        return response;
    }
}