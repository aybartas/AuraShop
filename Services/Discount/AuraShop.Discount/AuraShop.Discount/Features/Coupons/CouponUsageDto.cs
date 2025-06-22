namespace AuraShop.Discount.Features.Coupons;

public class CouponUsageDto
{
    public Guid Id { get; set; }
    public string CouponId { get; set; }
    public Guid UserId { get; set; }
    public DateTime AppliedAt { get; set; }
}