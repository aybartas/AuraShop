using AuraShop.Catalog.Database;
using AuraShop.Catalog.Features.Product;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace AuraShop.Catalog.Features.Seed
{
    public class SeedService
    {
        private readonly IMongoCollection<Category.Category> _categoryCollection;
        private readonly IMongoCollection<Product.Product> _productCollection;
        private readonly ILogger<SeedService> _logger;

        public SeedService(IMongoDatabase database, IOptions<DatabaseSettings> databaseSettings, ILogger<SeedService> logger)
        {
            var settings = databaseSettings.Value;
            _categoryCollection = database.GetCollection<Category.Category>(settings.CategoryCollectionName);
            _productCollection = database.GetCollection<Product.Product>(settings.ProductCollectionName);
            _logger = logger;
        }

        public async Task SeedAsync()
        {
            // Seed Categories
            try
            {
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
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }

        private List<Category.Category> GetPredefinedCategories()
        {
            return
            [
                new() { Id = Guid.NewGuid(), Name = "Electronics" },
                new() { Id = Guid.NewGuid(), Name = "Clothing" },
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
                        Name = "Smartphone X200",
                        Price = 699.99m,
                        CategoryId = electronics.Id,
                        Colors = null,
                        Sizes = null,
                        Category = electronics.Name,
                        Images = new List<string>
                        {
                            "https://picsum.photos/id/1010/300",
                            "https://picsum.photos/id/1011/300"
                        },
                        Description = "A powerful smartphone with 128GB storage and an amazing camera."
                    },
                new()
                {
                    Id = Guid.NewGuid(),
                    Name = "Noise Cancelling Headphones",
                    Price = 249.99m,
                    CategoryId = electronics.Id,
                    Colors = new List<ProductColor>
                    {
                        new() { Name = "Black", HexCode = "#000000" },
                        new() { Name = "Silver", HexCode = "#C0C0C0" }
                    },
                    Sizes = null,
                    Category = electronics.Name,
                    Images = new List<string>
                    {
                        "https://picsum.photos/id/1015/300",
                        "https://picsum.photos/id/1016/300"
                    },
                    Description = "Premium over-ear headphones with active noise cancellation and deep bass."
                },
                new()
                {
                    Id = Guid.NewGuid(),
                    Name = "4K Ultra HD Smart TV",
                    Price = 999.99m,
                    CategoryId = electronics.Id,
                    Colors = null,
                    Sizes = null,
                    Category = electronics.Name,
                    Images = new List<string>
                    {
                        "https://picsum.photos/id/1020/300",
                        "https://picsum.photos/id/1021/300"
                    },
                    Description = "A stunning 55-inch 4K smart TV with HDR support and streaming apps."
                },
                new()
                {
                    Id = Guid.NewGuid(),
                    Name = "Men's Cotton T-Shirt",
                    Price = 24.99m,
                    CategoryId = clothing.Id,
                    Colors = new List<ProductColor>
                    {
                        new() { Name = "White", HexCode = "#FFFFFF" },
                        new() { Name = "Black", HexCode = "#000000" }
                    },
                    Sizes = new List<string> { "S", "M", "L", "XL", "XXL" },
                    Category = clothing?.Name,
                    Images = new List<string>
                    {
                        "https://picsum.photos/id/1030/300",
                        "https://picsum.photos/id/1031/300"
                    },
                    Description = "Soft and breathable 100% cotton T-shirt, perfect for everyday wear."
                },
                new()
                {
                    Id = Guid.NewGuid(),
                    Name = "Women's Summer Dress",
                    Price = 39.99m,
                    CategoryId = clothing.Id,
                    Colors = new List<ProductColor>
                    {
                        new() { Name = "Floral", HexCode = "#FF69B4" },
                        new() { Name = "Navy Blue", HexCode = "#000080" }
                    },
                    Sizes = new List<string> { "XS", "S", "M", "L", "XL" },
                    Category = clothing?.Name,
                    Images = new List<string>
                    {
                        "https://picsum.photos/id/1040/300",
                        "https://picsum.photos/id/1041/300"
                    },
                    Description = "Lightweight and elegant summer dress with a beautiful floral print."
                },
                new()
                {
                    Id = Guid.NewGuid(),
                    Name = "Unisex Hoodie",
                    Price = 49.99m,
                    CategoryId = clothing.Id,
                    Colors = new List<ProductColor>
                    {
                        new() { Name = "Gray", HexCode = "#808080" },
                        new() { Name = "Green", HexCode = "#228B22" }
                    },
                    Sizes = new List<string> { "S", "M", "L", "XL", "XXL" },
                    Category = clothing?.Name,
                    Images = new List<string>
                    {
                        "https://picsum.photos/id/1050/300",
                        "https://picsum.photos/id/1051/300"
                    },
                    Description = "Cozy and stylish hoodie made from a premium cotton-blend fabric."
                }
            ];
        }
    }
}
