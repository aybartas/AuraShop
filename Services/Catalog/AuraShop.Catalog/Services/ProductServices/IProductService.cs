using AuraShop.Catalog.Dtos.ProductDtos;

namespace AuraShop.Catalog.Services.ProductServices
{
    public interface IProductService
    {
        Task<List<ProductDto>> GetAllCategoriesAsync();
        Task CreateProductAsync(CreateProductDto productDto);
        Task UpdateProductAsync(UpdateProductDto productDto);
        Task DeleteProductAsync(string id);
        Task<ProductDto> GetProductByIdAsync(string id);
    }
}
