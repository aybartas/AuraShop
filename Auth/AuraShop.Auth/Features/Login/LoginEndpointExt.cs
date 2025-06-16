using AuraShop.Shared.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace AuraShop.Auth.Features.Login
{
    public static class LoginEndpointExt 
    {
        public static WebApplication AddLoginEndpoint(this WebApplication app)
        {
            app.MapPost("/api/auth/login", async ([FromBody] LoginRequest req, LoginHandler handler) =>
            {
                var response = await handler.HandleAsync(req);
                return response.ToResult();
            });

            return app;
        }
    }
}
