using AuraShop.Shared.Auth;
using AuraShop.Shared.Extensions;
using AuraShop.Shared.Filters;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AuraShop.Catalog.Features.Product.Update
{
    public static class UpdateProductEndpoint
    {
        public static RouteGroupBuilder UpdateProduct(this RouteGroupBuilder group)
        {
            group.MapPut("/{id:guid}", async ([FromForm] UpdateProductCommand command, IMediator mediator) =>
            {
                var result = await mediator.Send(command);

                return result.ToResult();

            }).AddEndpointFilter<ValidationFilter<UpdateProductCommand>>().DisableAntiforgery().MapToApiVersion(1, 0).RequireAuthorization(Policies.AdminOnly);

            return group;
        }
    }
}
