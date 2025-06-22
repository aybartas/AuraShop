using AuraShop.Discount.Features.Coupons;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace AuraShop.Discount.Database
{
    public static class DatabaseExtensions
    {
        public static async Task AddDiscountDatabaseAsync(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<IMongoDatabase>();
            var settings = scope.ServiceProvider.GetRequiredService<IOptions<DatabaseSettings>>().Value;

            var couponsCollectionName = settings.CouponsCollectionName;

            var existingCollections = await (await db.ListCollectionNamesAsync()).ToListAsync();

            if (!existingCollections.Contains(couponsCollectionName))
            {
                await db.CreateCollectionAsync(couponsCollectionName);

                var couponCollection = db.GetCollection<Coupon>(couponsCollectionName);

                var indexKeys = Builders<Coupon>.IndexKeys
                    .Ascending(c => c.Code);

                var indexOptions = new CreateIndexOptions { Unique = true };
                var indexModel = new CreateIndexModel<Coupon>(indexKeys, indexOptions);

                await couponCollection.Indexes.CreateOneAsync(indexModel);
            }

            var couponUsageCollectionName = settings.CouponsCollectionName;

            if (!existingCollections.Contains(couponUsageCollectionName))
            {
                await db.CreateCollectionAsync(couponUsageCollectionName);

                var couponUsageCollection = db.GetCollection<CouponUsage>(couponUsageCollectionName);

                var indexKeys = Builders<CouponUsage>.IndexKeys
                    .Ascending(c => c.UserId);

                var indexModel = new CreateIndexModel<CouponUsage>(indexKeys);

                await couponUsageCollection.Indexes.CreateOneAsync(indexModel);
            }
        }
    }
}
