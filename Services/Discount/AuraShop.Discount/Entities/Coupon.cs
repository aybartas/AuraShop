namespace AuraShop.Discount.Entities
{
    public class Coupon
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public decimal Rate { get; set; }
        public bool IsActive { get; set; }
        public DateTime ExpireDate { get; set; }
    }
}
