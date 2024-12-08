using AuraShop.Catalog.Dtos.ProductDtos;
using AuraShop.Catalog.Models;

namespace AuraShop.Catalog.Services.ProductServices
{
    public interface IProductService
    {
        Task<List<ProductDto>> GetProducts(GetProductFilter filter);
        Task CreateProductAsync(CreateProductDto productDto);
        Task UpdateProductAsync(UpdateProductDto productDto);
        Task DeleteProductAsync(string id);
        Task<ProductDto> GetProductByIdAsync(string id);
    }
}
