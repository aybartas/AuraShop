using AuraShop.Basket.Data;

namespace AuraShop.Basket.Dtos;

public class BasketDto
{
    public List<BasketItem> BasketItems { get; set; } = [];
    public string? Coupon { get; set; }
    public decimal? DiscountRate { get; set; }
    public bool HasDiscount { get; set; }
    public decimal TotalPrice { get; set; }
    public decimal? TotalDiscountedPrice { get; set; }
}