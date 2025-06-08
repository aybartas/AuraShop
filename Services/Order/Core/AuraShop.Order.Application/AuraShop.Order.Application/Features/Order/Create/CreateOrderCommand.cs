using AuraShop.Order.Application.Dtos;
using AuraShop.Shared;
using MediatR;

namespace AuraShop.Order.Application.Features.Order.Create
{
    public class CreateOrderCommand : IRequest<ServiceResult<CreateOrderCommandResponse>>
    {
        public string? CouponCode { get; set; }
        public OrderAddressDto ShippingOrderAddress { get; set; } = null!;
        public bool SaveShippingAddress { get; set; }
        public List<OrderItemDto> Items { get; set; } = [];
    }
}
