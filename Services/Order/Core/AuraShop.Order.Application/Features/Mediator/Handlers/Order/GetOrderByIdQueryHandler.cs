using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuraShop.Order.Application.Features.Mediator.Queries.Order;
using AuraShop.Order.Application.Features.Mediator.Results.Order;
using AuraShop.Order.Application.Interfaces;
using MediatR;

namespace AuraShop.Order.Application.Features.Mediator.Handlers.Order
{
    public class GetOrderByIdQueryHandler : IRequestHandler<GetOrderByIdQuery,GetOrderByIdQueryResult>
    {
        private readonly IRepository<Domain.Entities.Order> _repository;

        public GetOrderByIdQueryHandler(IRepository<Domain.Entities.Order> repository)
        {
            _repository = repository;
        }

        public async Task<GetOrderByIdQueryResult> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
        {
            var order = await _repository.GetByIdAsync(request.Id);

            return new GetOrderByIdQueryResult
            {
                Id = order.Id,
                UserId = order.UserId,
                TotalPrice = order.TotalPrice,
                OrderDate = order.OrderDate
            };
        }
    }
}
