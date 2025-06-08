using AuraShop.Shared.Extensions;
using MediatR;

namespace AuraShop.Catalog.Features.Product.GetById
{
    public static class GetProductByIdEndpoint
    {
        public static RouteGroupBuilder GetProductById(this RouteGroupBuilder group)
        {
            group.MapGet("/{id:guid}", async (Guid id, IMediator mediator) =>
            {
                var query = new GetProductByIdQuery
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
