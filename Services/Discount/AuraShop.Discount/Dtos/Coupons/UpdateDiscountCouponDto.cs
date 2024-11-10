namespace AuraShop.Discount.Dtos.Coupons
{
    public class UpdateDiscountCouponDto
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public decimal Rate { get; set; }
        public bool IsActive { get; set; }
        public DateTime ExpireDate { get; set; }
    }
}
