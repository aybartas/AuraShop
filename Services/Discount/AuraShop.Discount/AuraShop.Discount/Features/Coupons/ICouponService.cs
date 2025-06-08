namespace AuraShop.Discount.Features.Coupons
{
    public interface ICouponService
    {
        Task<List<Coupon>> GetAllCategoriesAsync();
        Task CreateCouponAsync(Coupon coupon);
        Task<List<Coupon>> GetUserCoupons(Guid userId);
    }
}
