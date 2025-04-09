using AuraShop.Shared.Extensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AuraShop.Catalog.Features.Category.Create
{
    public static class CreateCategoryEndpoint
    {
        public static RouteGroupBuilder CreateCategoryEndpointGroupItem(this RouteGroupBuilder group)
        {
            group.MapPost("/", async (CreateCategoryCommand command, IMediator mediator) =>
            {
                var result = await mediator.Send(command);

                return result.ToResult();
            });

            return group;
        }
    }
}
