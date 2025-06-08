using AuraShop.Discount.Database;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace AuraShop.Discount.Features.Coupons
{
    public class CouponService(IMongoDatabase database, IOptions<DatabaseSettings> databaseSettings) : ICouponService
    {
        private readonly IMongoCollection<Coupon> _couponsCollection = database.GetCollection<Coupon>(databaseSettings.Value.CouponsCollectionName);

        public async Task<List<Coupon>> GetAllCategoriesAsync()
        {
            var categoriesCursor = await _couponsCollection.FindAsync(_ => true);
            var categories = await categoriesCursor.ToListAsync();

            return categories;
        }

        public async Task CreateCouponAsync(Coupon coupon)
        {
            await _couponsCollection.InsertOneAsync(coupon);
        }

        public async Task<List<Coupon>> GetUserCoupons(Guid userId )
        {
            var couponCursor = await _couponsCollection.FindAsync(x => x.UserId == userId);
            var coupons = await couponCursor.ToListAsync();
            return coupons;
        }
    }
}
