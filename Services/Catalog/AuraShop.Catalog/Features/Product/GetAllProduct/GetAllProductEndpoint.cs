using AuraShop.Shared.Extensions;
using MediatR;

namespace AuraShop.Catalog.Features.Product.GetAllProduct
{
    public static class GetAllProductEndpoint
    {
        public static RouteGroupBuilder GetAllProducts(this RouteGroupBuilder group)
        {
            group.MapGet("/", async (IMediator mediator) =>
            {
                var result = await mediator.Send(new GetAllProductQuery());

                return result.ToResult();

            }).MapToApiVersion(1, 0);

            return group;
        }
    }
}
