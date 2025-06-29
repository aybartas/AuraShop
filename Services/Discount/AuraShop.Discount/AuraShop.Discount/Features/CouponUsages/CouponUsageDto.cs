namespace AuraShop.Discount.Features.CouponUsages;

public class CouponUsageDto
{
    public Guid Id { get; set; }
    public Guid CouponId { get; set; }
    public Guid UserId { get; set; }
    public DateTime AppliedAt { get; set; }
}