using Asp.Versioning.Builder;

namespace AuraShop.Order.WebApi.Endpoints.Orders
{
    public static class OrderEndpointExt
    {
        public static void AddOrderGroupEndpoints(this WebApplication app, ApiVersionSet apiVersionSet)
        {
            app.MapGroup("api/v{version:apiVersion}/orders").WithTags("Order")
                .AddOrderGroupEndpointExt()
               .WithApiVersionSet(apiVersionSet);
        }
    }
}
