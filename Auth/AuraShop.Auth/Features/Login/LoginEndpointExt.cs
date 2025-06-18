using AuraShop.Shared.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace AuraShop.Auth.Features.Login
{
    public static class LoginEndpointExt 
    {
        public static RouteGroupBuilder AddLoginEndpoint(this RouteGroupBuilder groupBuilder)
        {
            groupBuilder.MapPost("/login", async ([FromBody] LoginRequest req, LoginHandler handler) =>
            {
                var response = await handler.HandleAsync(req);
                return response.ToResult();
            });

            return groupBuilder;
        }
    }
}
