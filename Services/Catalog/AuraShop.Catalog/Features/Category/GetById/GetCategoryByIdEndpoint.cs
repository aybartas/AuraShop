using AuraShop.Shared.Extensions;
using MediatR;

namespace AuraShop.Catalog.Features.Category.GetById
{
    public static class GetCategoryByIdEndpoint
    {
        public static RouteGroupBuilder GetCategoryById(this RouteGroupBuilder group)
        {
            group.MapPost("/{id:guid}", async (Guid id, IMediator mediator) =>
            {
                var query = new GetCategoryByIdQuery
                {
                    Id = id
                };

                var result = await mediator.Send(query);

                return result.ToResult();

            }).MapToApiVersion(1, 0);

            return group;
        }
    }
}
