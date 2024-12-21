using AuraShop.Catalog.Dtos.ProductDetailDtos;
using AuraShop.Catalog.Entities;
using AuraShop.Catalog.Settings;
using AutoMapper;
using MongoDB.Driver;

namespace AuraShop.Catalog.Services.ProductDetailsServices
{
    public class ProductDetailsService : IProductDetailsService
    {
        private readonly IMongoCollection<ProductDetail> _productDetailCollection;
        private readonly IMapper _mapper;
        public ProductDetailsService(IDatabaseSettings _databaseSettings, IMapper mapper)
        {
            var client = new MongoClient(_databaseSettings.ConnectionString);
            var database = client.GetDatabase(_databaseSettings.DatabaseName);

            _productDetailCollection = database.GetCollection<ProductDetail>(_databaseSettings.ProductDetailCollectionName);
            _mapper = mapper;
        }
        public async Task<List<ProductDetailDto>> GetAllProductDetailsAsync()
        {
            var values = await _productDetailCollection.FindAsync(x => true);
            var result = _mapper.Map<List<ProductDetailDto>>(values.ToListAsync());

            return result;
        }

        public async Task CreateProductDetailAsync(CreateProductDetailDto productDetailDto)
        {
            var value = _mapper.Map<ProductDetail>(productDetailDto);
            await _productDetailCollection.InsertOneAsync(value);
        }

        public async Task UpdateProductDetailAsync(UpdateProductDetailDto productDetailDto)
        {
            var value = _mapper.Map<ProductDetail>(productDetailDto);
            await _productDetailCollection.FindOneAndReplaceAsync(x => x.Id == value.Id, value);
        }

        public async Task DeleteProductDetailAsync(string id)
        {
            await _productDetailCollection.DeleteOneAsync(x => x.Id == id);
        }

        public async Task<ProductDetailDto> GetProductDetailByIdAsync(string id)
        {
            var values = await _productDetailCollection.FindAsync(x => x.Id == id);
            var result = _mapper.Map<ProductDetailDto>(values?.FirstOrDefault());
            return result;
        }
    }
}
