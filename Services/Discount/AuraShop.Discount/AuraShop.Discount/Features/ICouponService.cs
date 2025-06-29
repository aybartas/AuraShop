using AuraShop.Discount.Features.Coupons;
using AuraShop.Discount.Features.CouponUsages;

namespace AuraShop.Discount.Features
{
    public interface ICouponService
    {
        Task CreateCouponAsync(Coupon coupon);
        Task<List<Coupon>> GetCouponsAsync();
        Task CreateCouponUsageAsync(CouponUsage coupon);
        Task<Coupon> GetCouponByCode(string code);
        Task<List<CouponUsage>> GetCouponUsagesByUserIdAsync(Guid userId);
    }
}
