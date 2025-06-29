namespace AuraShop.Discount.Features.Coupons.Validate;

public class ValidateCouponQueryResponse
{
    public bool IsValid { get; set; }
    public decimal DiscountRate { get; set; }
    public string CouponCode { get; set; }
    public string ErrorMessage { get; set; }
}