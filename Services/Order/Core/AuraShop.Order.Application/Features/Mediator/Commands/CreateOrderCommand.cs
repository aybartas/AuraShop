
using AuraShop.Order.Domain.Entities;
using MediatR;

namespace AuraShop.Order.Application.Features.Mediator.Commands
{
    public class CreateOrderCommand : IRequest
    {
        public string UserId { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime OrderDate { get; set; }
        public List<OrderLine> OrderLines { get; set; }
    }
}
