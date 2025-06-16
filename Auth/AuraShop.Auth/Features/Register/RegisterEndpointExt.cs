using AuraShop.Shared.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace AuraShop.Auth.Features.Register;

public static class RegisterEndpointExt
{
    public static WebApplication AddRegisterEndpoint(this WebApplication app)
    {
        app.MapPost("/api/auth/register", async ([FromBody] RegisterRequest req, RegisterHandler handler) =>
        {
            var registerResponse = await handler.HandleAsync(req);
            return registerResponse.ToResult();
        });
        return app;
    }
}