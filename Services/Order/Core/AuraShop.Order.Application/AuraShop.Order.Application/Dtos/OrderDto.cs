using AuraShop.Order.Domain.Entities;

namespace AuraShop.Order.Application.Dtos;

public class OrderDto
{
    public string OrderNumber { get; set; } = null!;
    public Guid UserId { get; set; }
    public DateTime OrderDate { get; set; }
    public string? CouponCode { get; set; }
    public decimal? DiscountRate { get; set; }
    public OrderStatus Status { get; set; }
    public List<OrderItemDto> Items { get; set; } = new();
    public string? PaymentId { get; set; }
    public OrderAddressDto ShippingOrderAddress { get; set; } = null!;

    public decimal TotalPriceBeforeDiscount { get; set; }
    public decimal TotalPrice { get; set; }
}