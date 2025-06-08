using AuraShop.Shared.Extensions;
using MediatR;

namespace AuraShop.Catalog.Features.Product.Delete
{
    public static class DeleteProductEndpoint
    {
        public static RouteGroupBuilder DeleteProduct(this RouteGroupBuilder group)
        {
            group.MapDelete("/{id}", async (Guid id, IMediator mediator) =>
            {
                var result = await mediator.Send(new DeleteProductCommand(id));

                return result.ToResult();

            }).MapToApiVersion(1, 0);

            return group;
        }
    }
}
