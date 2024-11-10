using AuraShop.Catalog.Dtos.ProductDetailDtos;

namespace AuraShop.Catalog.Services.ProductDetailsServices
{
    public interface IProductDetailsService
    {
        Task<List<ProductDetailDto>> GetAllProductDetailsAsync();
        Task CreateProductDetailAsync(CreateProductDetailDto productDetailsDto);
        Task UpdateProductDetailAsync(UpdateProductDetailDto productDetailsDto);
        Task DeleteProductDetailAsync(string id);
        Task<ProductDetailDto> GetProductDetailByIdAsync(string id);
    }
}
