using AuraShop.Catalog.Features.Category;

namespace AuraShop.Catalog.Services.CategoryServices
{
    public interface ICategoryService
    {
        Task<List<CategoryDto>> GetAllCategoriesAsync();
        Task CreateCategoryAsync(CreateCategoryDto categoryDto);
        Task UpdateCategoryAsync(UpdateCategoryDto categoryDto);
        Task DeleteCategoryAsync(Guid id);
        Task<CategoryDto> GetCategoryByIdAsync(Guid id);
    }
}
