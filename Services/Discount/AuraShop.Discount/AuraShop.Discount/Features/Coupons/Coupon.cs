using AuraShop.Discount.Database;

namespace AuraShop.Discount.Features.Coupons
{
    public class Coupon : BaseEntity
    {
        public string Code { get; set; }
        public decimal Rate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public bool IsActive { get; set; }
    }
}
