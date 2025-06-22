using AuraShop.Shared.Extensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AuraShop.Auth.Features.Register;

public static class RegisterEndpointExt
{
    public static RouteGroupBuilder AddRegisterEndpoint(this RouteGroupBuilder builder)
    {
        builder.MapPost("/register", async ([FromBody] RegisterRequest req, IMediator mediator) =>
        {
            var registerResponse = await mediator.Send(req);
            return registerResponse.ToResult();
        });
        return builder;
    }
}