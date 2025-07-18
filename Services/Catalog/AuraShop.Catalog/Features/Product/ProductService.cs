﻿using AuraShop.Catalog.Database;
using AutoMapper;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace AuraShop.Catalog.Features.Product
{
    public class ProductService : IProductService
    {
        private readonly IMongoCollection<Product> _productCollection;
        public ProductService(IMongoDatabase database, IOptions<DatabaseSettings> databaseSettings, IMapper mapper)
        {
            _productCollection = database.GetCollection<Product>(databaseSettings.Value.ProductCollectionName);
        }
        public async Task<List<Product>> GetAllProductsAsync()
        {  
            var productsCursor = await _productCollection.FindAsync(x => true);
            var products = await productsCursor.ToListAsync();

            return products;
        }

        public async Task<Product> CreateProductAsync(Product product)
        {
            await _productCollection.InsertOneAsync(product);
            return product;
        }

        public async Task UpdateProductAsync(Product product)
        {
            await _productCollection.FindOneAndReplaceAsync(x => x.Id == product.Id, product);
        }

        public async Task DeleteProductAsync(Guid id)
        {
            await _productCollection.DeleteOneAsync(x => x.Id == id);
        }

        public async Task<Product> GetProductByIdAsync(Guid id)
        {
            var productCursor = await _productCollection.FindAsync(x => x.Id == id);
            var product = await productCursor.FirstOrDefaultAsync();
            return product;
        }
    }
}
