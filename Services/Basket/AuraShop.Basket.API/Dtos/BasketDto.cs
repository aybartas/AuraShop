using AuraShop.Basket.Data;

namespace AuraShop.Basket.Dtos;

public class BasketDto
{
    public List<BasketItemDto> BasketItems { get; set; } = [];
    public string? Coupon { get; set; }
    public decimal? DiscountRate { get; set; }
    public decimal ShippingAmount { get; set; }
    public bool HasDiscount { get; set; }
    public decimal TotalPrice { get; set; }
    public decimal? TotalDiscountedPrice { get; set; }
}

