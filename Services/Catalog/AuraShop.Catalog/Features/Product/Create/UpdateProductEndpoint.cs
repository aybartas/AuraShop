using AuraShop.Catalog.Features.Product.Update;
using AuraShop.Shared.Extensions;
using AuraShop.Shared.Filters;
using MediatR;

namespace AuraShop.Catalog.Features.Product.Create
{
    public static class UpdateProductEndpoint
    {
        public static RouteGroupBuilder CreateProduct(this RouteGroupBuilder group)
        {
            group.MapPost("/", async (CreateProductCommand command, IMediator mediator) =>
            {
                var result = await mediator.Send(command);

                return result.ToResult();

            }).AddEndpointFilter<ValidationFilter<UpdateProductCommand>>().MapToApiVersion(1, 0);

            return group;
        }
    }
}
