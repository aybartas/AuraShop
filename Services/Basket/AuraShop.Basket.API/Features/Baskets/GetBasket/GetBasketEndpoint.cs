using AuraShop.Shared.Extensions;
using MediatR;

namespace AuraShop.Basket.Features.Baskets.GetBasket
{
    public static class GetBasketEndpoint
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
