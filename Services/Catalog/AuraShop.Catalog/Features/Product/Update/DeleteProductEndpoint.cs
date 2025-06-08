using AuraShop.Shared.Extensions;
using AuraShop.Shared.Filters;
using MediatR;

namespace AuraShop.Catalog.Features.Product.Update
{
    public static class DeleteProductEndpoint
    {
        public static RouteGroupBuilder UpdateProduct(this RouteGroupBuilder group)
        {
            group.MapPut("/{id:guid}", async (UpdateProductCommand command, IMediator mediator) =>
            {
                var result = await mediator.Send(command);

                return result.ToResult();

            }).AddEndpointFilter<ValidationFilter<UpdateProductCommand>>().MapToApiVersion(1, 0);

            return group;
        }
    }
}
