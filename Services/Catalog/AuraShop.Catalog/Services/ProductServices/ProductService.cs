using AuraShop.Catalog.Dtos.ProductDtos;
using AuraShop.Catalog.Entities;
using AuraShop.Catalog.Models;
using AuraShop.Catalog.Settings;
using AuraShop.Catalog.Utils;
using AutoMapper;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace AuraShop.Catalog.Services.ProductServices
{
    public class ProductService : IProductService
    {
        private readonly IMongoCollection<Product> _productCollection;
        private readonly IMapper _mapper;
        public ProductService(IMongoDatabase mongoDatabase, IOptions<DatabaseSettings> _databaseSettings, IMapper mapper)
        {
            _productCollection = mongoDatabase.GetCollection<Product>(_databaseSettings.Value.ProductCollectionName);
            _mapper = mapper;
        }
        public async Task<List<ProductDto>> GetProducts(GetProductFilter filter)
        {
            var request = filter.GetProductFilterAndSortDefinition();

            var result = await _productCollection.Find(request.FilterDefinition)
                .Sort(request.SortDefinition)
                .Skip(filter.Page * filter.Size)
                .Limit(filter.Size)
                .ToListAsync();

            var productDtos = result.Select(x => _mapper.Map<ProductDto>(x)).ToList();

            return productDtos;
        }

        public async Task CreateProductAsync(CreateProductDto productDto)
        {
            var value = _mapper.Map<Product>(productDto);
            await _productCollection.InsertOneAsync(value);
        }

        public async Task UpdateProductAsync(UpdateProductDto productDto)
        {
            var product = _mapper.Map<Product>(productDto);
            await _productCollection.ReplaceOneAsync(x => x.Id == product.Id, product);
        }

        public async Task DeleteProductAsync(string id)
        {
            await _productCollection.DeleteOneAsync(x => x.Id == id);
        }

        public async Task<ProductDto> GetProductByIdAsync(string id)
        {
            var values = await _productCollection.FindAsync(x => x.Id == id);

            var result = _mapper.Map<ProductDto>(values?.FirstOrDefault());

            return result;

        }
    }
}
