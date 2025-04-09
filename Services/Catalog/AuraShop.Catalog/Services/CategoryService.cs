using AuraShop.Catalog.Features.Category;
using AuraShop.Catalog.Repositories;
using AutoMapper;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace AuraShop.Catalog.Services
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
        public async Task<List<Category>> GetAllCategoriesAsync()
        {
            var categoriesCursor = await _categoryCollection.FindAsync(_ => true);
            var categories = await categoriesCursor.ToListAsync();

            return categories;
        }

        public async Task CreateCategoryAsync(Category category)
        {
            await _categoryCollection.InsertOneAsync(category);
        }

        public async Task UpdateCategoryAsync(Category category)
        {
            await _categoryCollection.FindOneAndReplaceAsync(x => x.Id == category.Id, category);
        }

        public async Task DeleteCategoryAsync(Guid id)
        {
            await _categoryCollection.DeleteOneAsync(x => x.Id == id);
        }

        public async Task<Category> GetCategoryByIdAsync(Guid id)
        {
            var categoryCursor = await _categoryCollection.FindAsync(x => x.Id == id);
            var category = await categoryCursor.FirstOrDefaultAsync();
            return category;
        }

        public async Task<Category> GetCategoryByNameAsync(string name)
        {
            var categoryCursor = await _categoryCollection.FindAsync(x => x.Name == name);
            var category = await categoryCursor.FirstOrDefaultAsync();
            return category;
        }
    }
}
