using AuraShop.Discount.Dtos.Coupons;
using AuraShop.Discount.Entities;

namespace AuraShop.Discount.Services
{
    public interface IDiscountService
    {
        Task<List<Coupon>> GetAllDiscountCouponsAsync();
        Task CreateDiscountCouponAsync(Coupon discountCoupon);
        void UpdateDiscountCouponAsync(Coupon discountCoupon);
        Task DeleteDiscountCouponAsync(Coupon id);
        Task<Coupon> GetDiscountCouponById(int id);
    }
}
