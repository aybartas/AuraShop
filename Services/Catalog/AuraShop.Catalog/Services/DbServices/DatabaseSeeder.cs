using AuraShop.Catalog.Entities;
using AuraShop.Catalog.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace AuraShop.Catalog.Services.DbServices
{
    public class DatabaseSeeder
    {
        private readonly IMongoDatabase _database;
        private readonly DatabaseSettings _databaseSettings;

        public DatabaseSeeder(IMongoDatabase database, IOptions<DatabaseSettings> databaseSettings)
        {
            _database = database;
            _databaseSettings = databaseSettings.Value;
        }

        public async Task Seed()
        {
            Console.WriteLine("Database seeded started!");

            //await _database.DropCollectionAsync(_databaseSettings.BrandCollectionName);
            //await _database.DropCollectionAsync(_databaseSettings.CategoryCollectionName);
            //await _database.DropCollectionAsync(_databaseSettings.ProductCollectionName);

            var brandCollection = _database.GetCollection<Brand>(_databaseSettings.BrandCollectionName);
            var categoryCollection = _database.GetCollection<Category>(_databaseSettings.CategoryCollectionName);
            var productCollection = _database.GetCollection<Product>(_databaseSettings.ProductCollectionName);


            // Seed Brands
            var brands = new List<Brand>
            {
                new() { Id = ObjectId.GenerateNewId().ToString(), Name = "TechBrand" },
                new() { Id = ObjectId.GenerateNewId().ToString(), Name = "HomeBrand" },
                new() { Id = ObjectId.GenerateNewId().ToString(), Name = "FashionBrand" }
            };
            await brandCollection.InsertManyAsync(brands);

            var categories = new List<Category>
            {
                new()
                {
                    Id = ObjectId.GenerateNewId().ToString(),
                    Name = "Electronics",
                    SubCategories = new List<Category>
                    {
                        new() { Id = ObjectId.GenerateNewId().ToString(), Name = "Smartphones", SubCategories = new List<Category>() },
                        new() { Id = ObjectId.GenerateNewId().ToString(), Name = "Laptops", SubCategories = new List<Category>() }
                    }
                },
                new()
                {
                    Id = ObjectId.GenerateNewId().ToString(),
                    Name = "Home Appliances",
                    SubCategories = new List<Category>
                    {
                        new() { Id = ObjectId.GenerateNewId().ToString(), Name = "Kitchen", SubCategories = new List<Category>() }
                    }
                },
                new()
                {
                    Id = ObjectId.GenerateNewId().ToString(),
                    Name = "Fashion",
                    SubCategories = new List<Category>
                    {
                        new() { Id = ObjectId.GenerateNewId().ToString(), Name = "Clothing", SubCategories = new List<Category>() }
                    }
                }
            };
            await categoryCollection.InsertManyAsync(categories);

            // Seed Products
            var products = new List<Product>
            {
                new()
                {
                    Id = ObjectId.GenerateNewId().ToString(),
                    Name = "Smartphone Pro",
                    Price = 999.99M,
                    Description = "A premium smartphone with high-end features.",
                    Images = new List<string> { "https://via.placeholder.com/150", "https://via.placeholder.com/150" },
                    Brand = brands[0],
                    Category = categories[0].SubCategories[0]
                },
                new()
                {
                    Id = ObjectId.GenerateNewId().ToString(),
                    Name = "Laptop X",
                    Price = 1499.99M,
                    Description = "High-performance laptop for professionals.",
                    Images = new List<string> { "https://via.placeholder.com/150", "https://via.placeholder.com/150" },
                    Brand = brands[0],
                    Category = categories[0].SubCategories[1]
                },
            };

            await productCollection.InsertManyAsync(products);

            Console.WriteLine("Database seeded successfully!");
        }
    }
}
