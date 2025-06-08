using AuraShop.Shared.Extensions;
using MediatR;

namespace AuraShop.Catalog.Features.Category.GetAll
{
    public static class GetAllCategoryEndpoint
    {
        public static RouteGroupBuilder GetAllCategory(this RouteGroupBuilder group)
        {
            group.MapGet("/", async (IMediator mediator) =>
            {
                var result = await mediator.Send(new GetAllCategoryQuery());

                return result.ToResult();

            }).MapToApiVersion(1, 0);

            return group;
        }
    }
}
