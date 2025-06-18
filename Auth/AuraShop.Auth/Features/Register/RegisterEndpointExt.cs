using AuraShop.Shared.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace AuraShop.Auth.Features.Register;

public static class RegisterEndpointExt
{
    public static RouteGroupBuilder AddRegisterEndpoint(this RouteGroupBuilder builder)
    {
        builder.MapPost("/register", async ([FromBody] RegisterRequest req, RegisterHandler handler) =>
        {
            var registerResponse = await handler.HandleAsync(req);
            return registerResponse.ToResult();
        });
        return builder;
    }
}