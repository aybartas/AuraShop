using AuraShop.Catalog.Features.Category;
using AuraShop.Catalog.Features.Product;
using AuraShop.Catalog.Repositories;
using AutoMapper;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace AuraShop.Catalog.Services
{
    public class ProductService : IProductService
    {
        private readonly IMongoCollection<Product> _productCollection;
        private readonly IMapper _mapper;
        public ProductService(IMongoDatabase database, IOptions<DatabaseSettings> databaseSettings, IMapper mapper)
        {
            _productCollection = database.GetCollection<Product>(databaseSettings.Value.CategoryCollectionName);
            _mapper = mapper;
        }
        public async Task<List<ProductDto>> GetAllProductsAsync()
        {  
            var productsCursor = await _productCollection.FindAsync(x => true);
            var products = await productsCursor.ToListAsync();

            var result = _mapper.Map<List<ProductDto>>(products.ToList());
            return result;
        }

        public async Task<Product> CreateProductAsync(CreateProductDto productDto)
        {
            var product = _mapper.Map<Product>(productDto);

            product.Id = Guid.NewGuid();

            await _productCollection.InsertOneAsync(product);

            return product;
        }

        public async Task UpdateProductAsync(UpdateProductDto productDto)
        {
            var product = _mapper.Map<Product>(productDto);
            await _productCollection.FindOneAndReplaceAsync(x => x.Id == product.Id, product);
        }

        public async Task DeleteProductAsync(Guid id)
        {
            await _productCollection.DeleteOneAsync(x => x.Id == id);
        }

        public async Task<ProductDto> GetProductByIdAsync(Guid id)
        {
            var productCursor = await _productCollection.FindAsync(x => x.Id == id);
            var product = await productCursor.FirstOrDefaultAsync();

            var result = _mapper.Map<ProductDto>(product);
            return result;
        }
    }
}
