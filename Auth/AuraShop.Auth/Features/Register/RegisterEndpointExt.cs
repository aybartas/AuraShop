using Microsoft.AspNetCore.Mvc;

namespace AuraShop.Auth.Features.Register;

public static class RegisterEndpointExt 
{
    public static WebApplication AddRegisterEndpoint(this WebApplication app)
    {
        app.MapPost("/api/auth/register", async ( [FromBody]RegisterRequest req, RegisterHandler handler) =>
        {
            var userId = await handler.HandleAsync(req);
            return Results.Ok(new { UserId = userId });
        });
        return app;
    }
}