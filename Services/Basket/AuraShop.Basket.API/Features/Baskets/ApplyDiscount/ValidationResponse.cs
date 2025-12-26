namespace AuraShop.Basket.Features.Baskets.ApplyDiscount;

public class ValidationResponse
{
    public bool IsValid { get; set; }
    public string CouponCode { get; set; }
    public decimal DiscountRate { get; set; }
    public string? ErrorMessage { get; set; }
}