namespace AuraShop.Catalog.Features.Product
{
    public interface IProductService
    {
        Task<List<Product>> GetAllProductsAsync();
        Task<Product> CreateProductAsync(Product product);
        Task UpdateProductAsync(Product product);
        Task DeleteProductAsync(Guid id);
        Task<Product?> GetProductByIdAsync(Guid id);
    }
}
