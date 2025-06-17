using AuraShop.Shared.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace AuraShop.Auth.Features.GetProfile
{
    public static class GetProfileEndpointExt
    {
        public static WebApplication AddGetProfileEndpoint(this WebApplication app)
        {
            app.MapPost("/api/auth/profile", async ([FromBody] GetProfileRequest req, GetProfileHandler handler) =>
            {
                var response = await handler.HandleAsync(req);
                return response.ToResult();
            });

            return app;
        }
    }
}
