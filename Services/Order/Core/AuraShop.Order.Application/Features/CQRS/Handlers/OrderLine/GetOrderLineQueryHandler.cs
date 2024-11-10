using AuraShop.Order.Application.Features.CQRS.Results.OrderLine;
using AuraShop.Order.Application.Interfaces;
using AuraShop.Order.Domain.Entities;

namespace AuraShop.Order.Application.Features.CQRS.Handlers.OrderLine
{
    public class GetOrderLineQueryHandler
    {
        private readonly IRepository<Domain.Entities.OrderLine> _repository;
        public GetOrderLineQueryHandler(IRepository<Domain.Entities.OrderLine> orderLineRepository)
        {
            _repository = orderLineRepository;
        }
        public async Task<List<GetOrderLineQueryResult>> Handle()
        {
            var orderLines = await _repository.GetAllAsync();

            var response = orderLines.Select(address => new GetOrderLineQueryResult
            {
                Id = address.Id,
                ProductId = address.ProductId,
                ProductName = address.ProductName,
                ProductPrice = address.ProductPrice,
                ProductTotalPrice = address.ProductTotalPrice,
                OrderId = address.OrderId,

            }).ToList();

            return response;
        }
    }
}
