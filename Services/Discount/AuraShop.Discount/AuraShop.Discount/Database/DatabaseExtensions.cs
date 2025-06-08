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

            var collectionName = settings.CouponsCollectionName;

            var existingCollections = await db.ListCollectionNames().ToListAsync();
            if (!existingCollections.Contains(collectionName))
            {
                await db.CreateCollectionAsync(collectionName);

                var couponCollection = db.GetCollection<Coupon>(collectionName);

                var indexKeys = Builders<Coupon>.IndexKeys
                    .Ascending(c => c.Code)
                    .Ascending(c => c.UserId);

                var indexOptions = new CreateIndexOptions { Unique = true };
                var indexModel = new CreateIndexModel<Coupon>(indexKeys, indexOptions);

                await couponCollection.Indexes.CreateOneAsync(indexModel);
            }
        }
    }
}
