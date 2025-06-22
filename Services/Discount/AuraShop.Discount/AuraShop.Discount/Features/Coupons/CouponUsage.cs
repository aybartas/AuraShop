using AuraShop.Discount.Database;

namespace AuraShop.Discount.Features.Coupons;

public class CouponUsage : BaseEntity
{
    public string CouponId { get; set; }
    public Guid UserId { get; set; }
    public DateTime AppliedAt { get; set; }
}