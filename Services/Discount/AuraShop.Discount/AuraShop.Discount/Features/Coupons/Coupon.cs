using AuraShop.Discount.Database;

namespace AuraShop.Discount.Features.Coupons
{
    public class Coupon : BaseEntity
    {
        public Guid UserId { get; set; }
        public decimal Rate { get; set; }
        public string Code { get; set; }
        public DateTime ExpirationDate { get; set; }
        public bool IsUsed { get; set; }
    }
}
