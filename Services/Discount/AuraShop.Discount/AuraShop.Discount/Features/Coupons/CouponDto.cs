namespace AuraShop.Discount.Features.Coupons
{
    public class CouponDto
    {
        public Guid Id { get; set; }
        public decimal Rate { get; set; }
        public string Code { get; set; }
        public DateTime ExpirationDate { get; set; }
        public bool IsUsed { get; set; }
    }
}
