using AuraShop.Catalog.Dtos.ProductImageDtos;
using AuraShop.Catalog.Entities;
using AuraShop.Catalog.Settings;
using AutoMapper;
using MongoDB.Driver;

namespace AuraShop.Catalog.Services.ProductImageServices
{
    public class ProductImageService : IProductImageService
    {
        private readonly IMongoCollection<ProductImage> _productDetailCollection;
        private readonly IMapper _mapper;
        public ProductImageService(IDatabaseSettings _databaseSettings, IMapper mapper)
        {
            var client = new MongoClient(_databaseSettings.ConnectionString);
            var database = client.GetDatabase(_databaseSettings.DatabaseName);

            _productDetailCollection = database.GetCollection<ProductImage>(_databaseSettings.ProductImageCollectionName);
            _mapper = mapper;
        }
        public async Task<List<ProductImageDto>> GetAllProductImagesAsync()
        {
            var values = await _productDetailCollection.FindAsync(x => true);
            var result = _mapper.Map<List<ProductImageDto>>(values);

            return result;
        }

        public async Task CreateProductImageAsync(CreateProductImageDto productImageDto)
        {
            var value = _mapper.Map<ProductImage>(productImageDto);
            await _productDetailCollection.InsertOneAsync(value);
        }

        public async Task UpdateProductImageAsync(UpdateProductImageDto productImageDto)
        {
            var value = _mapper.Map<ProductImage>(productImageDto);
            await _productDetailCollection.FindOneAndReplaceAsync(x => x.Id == value.Id, value);
        }

        public async Task DeleteProductImageAsync(string id)
        {
            await _productDetailCollection.DeleteOneAsync(x => x.Id == id);
        }

        public async Task<ProductImageDto> GetProductImageByIdAsync(string id)
        {
            var values = await _productDetailCollection.FindAsync(x => x.Id == id);
            var result = _mapper.Map<ProductImageDto>(values?.FirstOrDefault());
            return result;
        }
    }
}
