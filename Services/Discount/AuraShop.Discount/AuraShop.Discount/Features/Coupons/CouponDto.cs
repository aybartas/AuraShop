namespace AuraShop.Discount.Features.Coupons
{
    public class CouponDto
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public decimal DiscountRate { get; set; }
        public DateTime ExpireDate { get; set; }
    }
}
