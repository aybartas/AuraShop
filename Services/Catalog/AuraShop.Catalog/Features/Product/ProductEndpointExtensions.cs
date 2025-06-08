using Asp.Versioning.Builder;
using AuraShop.Catalog.Features.Product.Create;
using AuraShop.Catalog.Features.Product.Delete;
using AuraShop.Catalog.Features.Product.GetAll;
using AuraShop.Catalog.Features.Product.GetById;
using AuraShop.Catalog.Features.Product.Update;

namespace AuraShop.Catalog.Features.Product
{
    public static class ProductEndpointExtensions
    {
        public static void AddProductEndpoints(this WebApplication app , ApiVersionSet apiVersionSet)
        {
            app.MapGroup("api/v{version:apiVersion}/products").WithTags("Products")
                .CreateProduct()
                .GetAllProducts()
                .GetProductById()
                .UpdateProduct()
                .DeleteProduct()
                .WithApiVersionSet(apiVersionSet);
        }
    }
}
