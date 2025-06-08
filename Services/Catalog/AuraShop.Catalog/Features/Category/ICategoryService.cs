namespace AuraShop.Catalog.Features.Category
{
    public interface ICategoryService
    {
        Task<List<Category>> GetAllCategoriesAsync();
        Task CreateCategoryAsync(Category category);
        Task UpdateCategoryAsync(Category category);
        Task DeleteCategoryAsync(Guid id);
        Task<Category?> GetCategoryByIdAsync(Guid id);
        Task<Category> GetCategoryByNameAsync(string name);

    }
}
