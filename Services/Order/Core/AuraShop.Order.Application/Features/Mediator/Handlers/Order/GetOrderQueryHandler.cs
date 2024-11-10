using AuraShop.Order.Application.Features.Mediator.Queries.Order;
using AuraShop.Order.Application.Features.Mediator.Results.Order;
using AuraShop.Order.Application.Interfaces;
using MediatR;

namespace AuraShop.Order.Application.Features.Mediator.Handlers.Order
{
    public class GetOrderQueryHandler : IRequestHandler<GetOrderQuery,List<GetOrderQueryResult>>
    {
        private readonly IRepository<Domain.Entities.Order>  _repository;
        public GetOrderQueryHandler(IRepository<Domain.Entities.Order> repository)
        {
            _repository = repository;
        }

        public async Task<List<GetOrderQueryResult>> Handle(GetOrderQuery request, CancellationToken cancellationToken)
        {
            var values = await _repository.GetAllAsync();
            return (List<GetOrderQueryResult>)values.Select(x => new GetOrderQueryResult
            {
                Id = x.Id,
                UserId = x.UserId,
                TotalPrice = x.TotalPrice,
                OrderDate = x.OrderDate
            }).ToList();
        }
    }
}
