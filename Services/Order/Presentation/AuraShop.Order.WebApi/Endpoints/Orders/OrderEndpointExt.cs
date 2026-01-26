using Asp.Versioning.Builder;
using AuraShop.Shared.Auth;

namespace AuraShop.Order.API.Endpoints.Orders
{
    public static class OrderEndpointExt
    {
        public static void AddOrderGroupEndpoints(this WebApplication app, ApiVersionSet apiVersionSet)
        {
            app.MapGroup("api/v{version:apiVersion}/orders")
                .WithTags("Order")
                .RequireAuthorization(Policies.Authenticated)
                .AddCreateOrderEndpoint()
                .AddGetOrdersEndpoint()
                .WithApiVersionSet(apiVersionSet);
        }
    }
}
