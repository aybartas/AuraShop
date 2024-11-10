using AuraShop.Discount.Dtos.Coupons;

namespace AuraShop.Discount.Services
{
    public interface IDiscountService
    {
        Task<List<DiscountCouponDto>> GetAllDiscountCouponsAsync();
        Task CreateDiscountCouponAsync(CreateDiscountCouponDto discountCouponDto);
        Task UpdateDiscountCouponAsync(UpdateDiscountCouponDto discountCouponDto);
        Task DeleteDiscountCouponAsync(int id);
        Task<DiscountCouponDto> GetDiscountCouponById(int id);
    }
}
