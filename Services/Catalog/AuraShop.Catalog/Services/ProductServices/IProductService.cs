using AuraShop.Catalog.Features.Product;

namespace AuraShop.Catalog.Services.ProductServices
{
    public interface IProductService
    {
        Task<List<ProductDto>> GetAllProductsAsync();
        Task<Product> CreateProductAsync(CreateProductDto productDto);
        Task UpdateProductAsync(UpdateProductDto productDto);
        Task DeleteProductAsync(Guid id);
        Task<ProductDto> GetProductByIdAsync(Guid id);
    }
}
