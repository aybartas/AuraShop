using AuraShop.Discount.Database;

namespace AuraShop.Discount.Features.CouponUsages
{
    public class CouponUsage : BaseEntity
    {
        public Guid CouponId { get; set; }
        public Guid UserId { get; set; }
        public DateTime AppliedAt { get; set; }
    }
}

