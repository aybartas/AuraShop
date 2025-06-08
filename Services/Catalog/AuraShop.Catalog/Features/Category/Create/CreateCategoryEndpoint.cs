using Asp.Versioning.Builder;
using AuraShop.Shared.Extensions;
using AuraShop.Shared.Filters;
using MediatR;

namespace AuraShop.Catalog.Features.Category.Create
{
    public static class CreateCategoryEndpoint
    {
        public static RouteGroupBuilder CreateCategory(this RouteGroupBuilder group)
        {
            group.MapPost("/", async (CreateCategoryCommand command, IMediator mediator) =>
            {
                var result = await mediator.Send(command);

                return result.ToResult();

            }).MapToApiVersion(1,0).AddEndpointFilter<ValidationFilter<CreateCategoryCommand>>();

            return group;
        }
    }
}
