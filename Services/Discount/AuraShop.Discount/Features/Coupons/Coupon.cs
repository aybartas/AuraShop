using AuraShop.Discount.Database;

namespace AuraShop.Discount.Features.Coupons
{
    public class Coupon : BaseEntity
    {
        public string Code { get; set; }
        public decimal DiscountRate { get; set; }
        public DateTime ExpireDate { get; set; }
    }
}
