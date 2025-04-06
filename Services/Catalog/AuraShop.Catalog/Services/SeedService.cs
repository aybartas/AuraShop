using AuraShop.Catalog.Features.Category;
using AuraShop.Catalog.Features.Product;
using AuraShop.Catalog.Repositories;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace AuraShop.Catalog.Services
{
    public class SeedService
    {
        private readonly IMongoCollection<Category> _categoryCollection;
        private readonly IMongoCollection<Product> _productCollection;
        private readonly ILogger<SeedService> _logger;

        public SeedService(IMongoDatabase database,IOptions<DatabaseSettings> databaseSettings, ILogger<SeedService> logger)
        {
            var settings = databaseSettings.Value;
            _categoryCollection = database.GetCollection<Category>(settings.CategoryCollectionName);
            _productCollection = database.GetCollection<Product>(settings.ProductCollectionName);
            _logger = logger;
        }

        public async Task SeedAsync()
        {
            // Seed Categories
            if (await _categoryCollection.CountDocumentsAsync(FilterDefinition<Category>.Empty) == 0)
            {
                _logger.LogInformation("Seeding categories...");
                var categories = GetPredefinedCategories();
                await _categoryCollection.InsertManyAsync(categories);
            }
            else
            {
                _logger.LogInformation("Categories already seeded.");
            }

            // Seed Products
            if (await _productCollection.CountDocumentsAsync(FilterDefinition<Product>.Empty) == 0)
            {
                _logger.LogInformation("Seeding products...");
                var products = GetPredefinedProducts();
                await _productCollection.InsertManyAsync(products);
            }
            else
            {
                _logger.LogInformation("Products already seeded.");
            }
        }

        private List<Category> GetPredefinedCategories()
        {
            return new List<Category>
            {
                new() { Id = Guid.NewGuid(), Name = "Electronics" },
                new() { Id = Guid.NewGuid(), Name = "Clothing" },
                new() { Id = Guid.NewGuid(), Name = "Home Appliances" }
            };
        }

        private List<Product> GetPredefinedProducts()
        {
            // Assuming Category IDs will be dynamically fetched
            var category1Id = Guid.NewGuid().ToString();
            var category2Id = Guid.NewGuid().ToString();

            return new List<Product>{
                    new()
                    {
                        Id = Guid.NewGuid(),
                        Name = "Smartphone",
                        Price = 599.99m,
                        CategoryId = category1Id,
                        Category = "Electronics",
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
                        CategoryId = category2Id,
                        Category = "Clothing",
                        Images = new List<string>
                        {
                            "https://via.placeholder.com/400x300?text=Shirt+Image+1",
                            "https://via.placeholder.com/400x300?text=Shirt+Image+2"
                        },
                        Description = "A comfortable cotton T-shirt"
                    }
                };
        }
    }
}
