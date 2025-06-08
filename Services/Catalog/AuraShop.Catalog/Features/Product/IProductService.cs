namespace AuraShop.Catalog.Features.Product
{
    public interface IProductService
    {
        Task<List<Product>> GetAllProductsAsync();
        Task<Product> CreateProductAsync(Product productDto);
        Task UpdateProductAsync(Product productDto);
        Task DeleteProductAsync(Guid id);
        Task<Product?> GetProductByIdAsync(Guid id);
    }
}
