using AuraShop.Catalog.Database;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace AuraShop.Catalog.Features.Seed
{
    public class SeedService
    {
        private readonly IMongoCollection<Category.Category> _categoryCollection;
        private readonly IMongoCollection<Product.Product> _productCollection;
        private readonly ILogger<SeedService> _logger;

        public SeedService(IMongoDatabase database,IOptions<DatabaseSettings> databaseSettings, ILogger<SeedService> logger)
        {
            var settings = databaseSettings.Value;
            _categoryCollection = database.GetCollection<Category.Category>(settings.CategoryCollectionName);
            _productCollection = database.GetCollection<Product.Product>(settings.ProductCollectionName);
            _logger = logger;
        }

        public async Task SeedAsync()
        {
            // Seed Categories
            if (await _categoryCollection.CountDocumentsAsync(FilterDefinition<Category.Category>.Empty) == 0)
            {
                _logger.LogInformation("Seeding categories...");
                var seedCategories = GetPredefinedCategories();
                await _categoryCollection.InsertManyAsync(seedCategories);
            }
            else
            {
                _logger.LogInformation("Categories already seeded.");
            }

            var categories = await _categoryCollection.Find(FilterDefinition<Category.Category>.Empty).ToListAsync();

            // Seed Products
            if (await _productCollection.CountDocumentsAsync(FilterDefinition<Product.Product>.Empty) == 0)
            {
                _logger.LogInformation("Seeding products...");
                var products = GetPredefinedProducts(categories);
                await _productCollection.InsertManyAsync(products);
            }
            else
            {
                _logger.LogInformation("Products already seeded.");
            }
        }

        private List<Category.Category> GetPredefinedCategories()
        {
            return
            [
                new() { Id = Guid.NewGuid(), Name = "Electronics" },
                new() { Id = Guid.NewGuid(), Name = "Clothing" },
                new() { Id = Guid.NewGuid(), Name = "Home Appliances" }
            ];
        }

        private List<Product.Product> GetPredefinedProducts(List<Category.Category> categories)
        {

            var electronics = categories.FirstOrDefault(c => c.Name == "Electronics");
            var clothing = categories.FirstOrDefault(c => c.Name == "Clothing");

            return
            [
                new()
                {
                    Id = Guid.NewGuid(),
                    Name = "Smartphone",
                    Price = 599.99m,
                    CategoryId = electronics.Id,
                    Category = electronics.Name,
                    Images = new List<string>
                    {
                        "https://via.placeholder.com/400x300?text=Smartphone+Image+1",
                        "https://via.placeholder.com/400x300?text=Smartphone+Image+2"
                    },
                    Description = "A high-end smartphone"
                },

                new()
                {
                    Id = Guid.NewGuid(),
                    Name = "T-Shirt",
                    Price = 19.99m,
                    CategoryId = clothing.Id,
                    Category = clothing?.Name,
                    Images = new List<string>
                    {
                        "https://via.placeholder.com/400x300?text=Shirt+Image+1",
                        "https://via.placeholder.com/400x300?text=Shirt+Image+2"
                    },
                    Description = "A comfortable cotton T-shirt"
                }
            ];
        }
    }
}
