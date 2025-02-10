using AuraShop.Discount.Context;
using AuraShop.Discount.Entities;
using Microsoft.EntityFrameworkCore;

namespace AuraShop.Discount.Services
{
    public class DiscountService : IDiscountService
    {
        private readonly DapperContext _context;

        public DiscountService(DapperContext context)
        {
            _context = context;
        }

        public async Task<List<Coupon>> GetAllDiscountCouponsAsync()
        {
            var coupons = await _context.Coupons.ToListAsync();
            return coupons;
        }

        public async Task CreateDiscountCouponAsync(Coupon coupon)
        {
            await _context.Coupons.AddAsync(coupon);
            await _context.SaveChangesAsync();
        }

        public void  UpdateDiscountCouponAsync(Coupon coupon)
        {
            _context.Coupons.Update(coupon);
            _context.SaveChanges();
        }

        public async Task DeleteDiscountCouponAsync(Coupon coupon)
        {
            _context.Coupons.Remove(coupon);
            _context.SaveChanges();
        }

        public async Task<Coupon> GetDiscountCouponById(int id)
        {
            var coupon = await _context.Coupons.FindAsync(id);
            return coupon;     
        }

      
    }
}
