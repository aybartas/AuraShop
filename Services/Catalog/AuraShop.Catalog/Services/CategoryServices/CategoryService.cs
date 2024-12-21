using AuraShop.Catalog.Dtos.CategoryDtos;
using AuraShop.Catalog.Entities;
using AuraShop.Catalog.Settings;
using AutoMapper;
using Microsoft.AspNetCore.DataProtection.XmlEncryption;
using MongoDB.Driver;

namespace AuraShop.Catalog.Services.CategoryServices
{
    public class CategoryService : ICategoryService
    {
        private readonly IMongoCollection<Category> _categoryCollection;
        private readonly IMapper _mapper;
        public CategoryService(IDatabaseSettings _databaseSettings, IMapper mapper)
        {
            var client = new MongoClient(_databaseSettings.ConnectionString);
            var database = client.GetDatabase(_databaseSettings.DatabaseName);

            _categoryCollection = database.GetCollection<Category>(_databaseSettings.CategoryCollectionName);
            _mapper = mapper;
        }
        public async Task<List<CategoryDto>> GetAllCategoriesAsync()
        {
            var cursor = await _categoryCollection.FindAsync(_ => true);
            var values = cursor?.ToList() ?? new();

            var result = _mapper.Map<List<CategoryDto>>(values);

            return result;
        }

        public async Task CreateCategoryAsync(CreateCategoryDto categoryDto)
        {
            var value = _mapper.Map<Category>(categoryDto);
            await _categoryCollection.InsertOneAsync(value);
        }

        public async Task UpdateCategoryAsync(UpdateCategoryDto categoryDto)
        {
            var value = _mapper.Map<Category>(categoryDto);
            await _categoryCollection.FindOneAndReplaceAsync(x => x.Id == value.Id, value);
        }

        public async Task DeleteCategoryAsync(string id)
        {
            await _categoryCollection.DeleteOneAsync(x => x.Id == id);
        }

        public async Task<CategoryDto> GetCategoryByIdAsync(string id)
        {
            var values = await _categoryCollection.FindAsync(x => x.Id == id);
            var result = _mapper.Map<CategoryDto>(values?.FirstOrDefault());
            return result;
        }
    }
}
