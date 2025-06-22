using AuraShop.Basket.Features.Baskets.DeleteBasketItem;
using AuraShop.Shared.Extensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AuraShop.Basket.Features.Baskets.UpdateBasketItem
{
    public static class UpdateBasketItemEndpointExt
    {
        public static RouteGroupBuilder UpdateBasketItemEndpoint(this RouteGroupBuilder group)
        {
            group.MapPut("/items/{productId:guid}", async (UpdateBasketItemCommand command, IMediator mediator) =>
                {
                    var result = await mediator.Send(command);
                    return result.ToResult();

                }).WithName("UpdateBasketItem")
                .MapToApiVersion(1, 0);

            return group;
        }
    }
}
