using System.Text.Json.Serialization;

namespace AuraShop.Basket.Data;

public class Basket
{
    public List<BasketItem> BasketItems { get; set; } = [];
    public string? Coupon { get; set; }
    public decimal? DiscountRate { get; set; }
    public decimal ShippingAmount => BasketItems.Sum(item => item.Price * item.Quantity) > 500 ? 0 : 19.99m;
    public decimal Subtotal => BasketItems.Sum(item => item.Price * item.Quantity);
    public decimal TotalPrice => DiscountRate.HasValue? Subtotal + ShippingAmount * (1 - (DiscountRate.Value / 100)) : Subtotal + ShippingAmount;
    
    public void ApplyDiscount(string coupon, decimal discountRate)
    {
        Coupon = coupon;
        DiscountRate = discountRate;
    }

    public void RemoveDiscount()
    {
        DiscountRate = null;
        Coupon = null;
    }

}