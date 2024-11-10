using AuraShop.Catalog.Dtos.ProductImageDtos;

namespace AuraShop.Catalog.Services.ProductImageServices
{
    public interface IProductImageService
    {
        Task<List<ProductImageDto>> GetAllCProductImagesAsync();
        Task CreateProductImageAsync(CreateProductImageDto productDetailsDto);
        Task UpdateProductImageAsync(UpdateProductImageDto productDetailsDto);
        Task DeleteProductImageAsync(string id);
        Task<ProductImageDto> GetProductImageByIdAsync(string id);
    }
}
