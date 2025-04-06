using AuraShop.Catalog.Features.Category;
using AuraShop.Catalog.Repositories;
using AutoMapper;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace AuraShop.Catalog.Services.CategoryServices
{
    public class CategoryService : ICategoryService
    {
        private readonly IMongoCollection<Category> _categoryCollection;
        private readonly IMapper _mapper;
        public CategoryService(IMongoDatabase database, IOptions<DatabaseSettings> databaseSettings, IMapper mapper)
        {
            _categoryCollection = database.GetCollection<Category>(databaseSettings.Value.CategoryCollectionName);
            _mapper = mapper;
        }
        public async Task<List<CategoryDto>> GetAllCategoriesAsync()
        {
            var categoriesCursor = await _categoryCollection.FindAsync(_ => true);
            var categories = await categoriesCursor.ToListAsync();
            var result = _mapper.Map<List<CategoryDto>>(categories);

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

        public async Task DeleteCategoryAsync(Guid id)
        {
            await _categoryCollection.DeleteOneAsync(x => x.Id == id);
        }

        public async Task<CategoryDto> GetCategoryByIdAsync(Guid id)
        {
            var categoryCursor = await _categoryCollection.FindAsync(x => x.Id == id);
            var category = await categoryCursor.FirstOrDefaultAsync();

            return _mapper.Map<CategoryDto>(category);
        }
    }
}
