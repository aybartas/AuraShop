using Asp.Versioning.Builder;
using AuraShop.Catalog.Features.Category.Create;
using AuraShop.Catalog.Features.Category.GetAll;
using AuraShop.Catalog.Features.Category.GetById;

namespace AuraShop.Catalog.Features.Category
{
    public static class CategoryEndpointExt
    {
        public static void AddCategoryEndpoints(this WebApplication app, ApiVersionSet apiVersionSet)
        {
            app.MapGroup("api/v{version:apiVersion}/categories").WithTags("Category")
                .CreateCategory()
                .GetAllCategory()
                .GetCategoryById().WithApiVersionSet(apiVersionSet);
        }
    }
}
