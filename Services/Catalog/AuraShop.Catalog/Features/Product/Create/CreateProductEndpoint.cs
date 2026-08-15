using AuraShop.Catalog.Features.Product.Update;
using AuraShop.Shared.Auth;
using AuraShop.Shared.Extensions;
using AuraShop.Shared.Filters;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AuraShop.Catalog.Features.Product.Create
{
    public static class CreateProductEndpoint
    {
        public static RouteGroupBuilder CreateProduct(this RouteGroupBuilder group)
        {
            group.MapPost("/", async ([FromForm] CreateProductCommand command, IMediator mediator) =>
            {
                var result = await mediator.Send(command);

                return result.ToResult();

            }).AddEndpointFilter<ValidationFilter<CreateProductCommand>>().DisableAntiforgery().MapToApiVersion(1, 0).RequireAuthorization(Policies.AdminOnly);

            return group;
        }
    }
}
