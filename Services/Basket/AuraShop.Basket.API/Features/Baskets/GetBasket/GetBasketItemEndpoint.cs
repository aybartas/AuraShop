using AuraShop.Shared.Extensions;
using MediatR;

namespace AuraShop.Basket.API.Features.Baskets.GetBasket
{
    public static class GetBasketItemEndpoint
    {
        public static RouteGroupBuilder GetBasketItemGroupEndpoint(this RouteGroupBuilder group)
        {
            group.MapGet("/user", async (IMediator mediator) =>
                {
                    var result = await mediator.Send(new GetBasketQuery());
                    return result.ToResult();
                })
                .WithName("GetBasket")
                .MapToApiVersion(1, 0);

            return group;
        }
    }
}
