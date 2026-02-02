using AuraShop.Shared.Extensions;
using AuraShop.Shared.Filters;
using MediatR;

namespace AuraShop.Basket.Features.Baskets.AddBasketItem
{
    public static class AddBasketItemEndpoint
    {
        public static RouteGroupBuilder AddBasketItemGroupEndpoint(this RouteGroupBuilder group)
        {
            group.MapPost("/items", async (AddBasketItemCommand command, IMediator mediator) =>
            {
                var result = await mediator.Send(command);

                return result.ToResult();

            }).WithName("AddBasketItem")
                .MapToApiVersion(1, 0)
                .AddEndpointFilter<ValidationFilter<AddBasketItemCommand>>();

            return group;
        }
    }
}
