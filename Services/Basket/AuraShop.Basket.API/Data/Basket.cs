using System.Text.Json.Serialization;

namespace AuraShop.Basket.API.Data;

public class Basket
{
    public Guid UserId { get; set; }
    public List<BasketItem> BasketItems { get; set; } = [];
    public string? Coupon { get; set; }
    public decimal? DiscountRate { get; set; }

    [JsonIgnore]
    public bool HasDiscount => DiscountRate is > 0 && string.IsNullOrEmpty(Coupon);

    [JsonIgnore]
    public decimal TotalPrice => BasketItems.Sum(item => item.Price * item.Quantity);

    [JsonIgnore]
    public decimal? TotalDiscountedPrice => HasDiscount ?  BasketItems.Sum(item => item.DiscountedPrice.Value * item.Quantity) : null;
    public void ApplyDiscount(string coupon, decimal discountRate)
    {
        Coupon = coupon;
        DiscountRate = discountRate;

        foreach (var item in BasketItems)
        {
            item.DiscountedPrice = item.Price - (item.Price * discountRate / 100);
        }
    }

    public void ReApplyDiscount()
    {
        foreach (var item in BasketItems)
        {
            item.DiscountedPrice = item.Price - (item.Price * DiscountRate / 100);
        }
    }
    public void RemoveDiscount()
    {
        DiscountRate = null;
        Coupon = null;

        foreach (var item in BasketItems)
        {
            item.DiscountedPrice = null;
        }
    }
}