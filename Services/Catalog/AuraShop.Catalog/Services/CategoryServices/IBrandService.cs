using AuraShop.Catalog.Dtos.CategoryDtos;

namespace AuraShop.Catalog.Services.CategoryServices;

public interface IBrandService
{
    Task<List<CategoryDto>> GetAllBrandsAsync();
    Task CreateCategoryAsync(CreateCategoryDto categoryDto);
    Task UpdateCategoryAsync(UpdateCategoryDto categoryDto);
    Task DeleteCategoryAsync(string id);
    Task<CategoryDto> GetCategoryByIdAsync(string id);
}