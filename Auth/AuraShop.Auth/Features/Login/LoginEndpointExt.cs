using AuraShop.Shared.Extensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AuraShop.Auth.Features.Login
{
    public static class LoginEndpointExt 
    {
        public static RouteGroupBuilder AddLoginEndpoint(this RouteGroupBuilder groupBuilder)
        {
            groupBuilder.MapPost("/login", async ([FromBody] LoginRequest req, IMediator mediator) =>
            {
                var result = await mediator.Send(req);
                return result.ToResult();
            });

            return groupBuilder;
        }
    }
}
