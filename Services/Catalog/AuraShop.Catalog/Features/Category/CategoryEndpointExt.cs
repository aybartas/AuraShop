using AuraShop.Catalog.Features.Category.Create;

namespace AuraShop.Catalog.Features.Category
{
    public static class CategoryEndpointExt
    {
        public static void AddCategoryEndpoints(this WebApplication app)
        {
            app.MapGroup("api/categories").CreateCategoryEndpointGroupItem();
        }
    }
}
