using AuraShop.Discount.Database;
using AuraShop.Discount.Features.Coupons;
using AuraShop.Discount.Features.CouponUsages;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace AuraShop.Discount.Features
{
    public class CouponService(IMongoDatabase database, IOptions<DatabaseSettings> databaseSettings) : ICouponService
    {
        private readonly IMongoCollection<Coupon> _couponsCollection = database.GetCollection<Coupon>(databaseSettings.Value.CouponsCollectionName);
        private readonly IMongoCollection<CouponUsage> _couponUsageCollection = database.GetCollection<CouponUsage>(databaseSettings.Value.CouponUsageCollectionName);

        public async Task CreateCouponAsync(Coupon coupon)
        {
            await _couponsCollection.InsertOneAsync(coupon);
        }
        public async Task<List<Coupon>> GetCouponsAsync()
        {
            var couponCursor = await _couponsCollection.FindAsync(x => true);
            var coupons = await couponCursor.ToListAsync();
            return coupons;
        }

    
        public async Task<Coupon> GetCouponByCode(string code)
        {
            var couponCursor = await _couponsCollection.FindAsync(x => x.Code == code);
            var coupon = await couponCursor.FirstOrDefaultAsync();
            return coupon;
        }
        public async Task CreateCouponUsageAsync(CouponUsage coupon)
        {
            await _couponUsageCollection.InsertOneAsync(coupon);
        }

        public async Task<List<CouponUsage>> GetCouponUsagesByUserIdAsync(Guid userId)
        {
            var couponCursor = await _couponUsageCollection.FindAsync(x => x.UserId == userId );
            var coupons = await couponCursor.ToListAsync();
            return coupons;
        }

    }
}
