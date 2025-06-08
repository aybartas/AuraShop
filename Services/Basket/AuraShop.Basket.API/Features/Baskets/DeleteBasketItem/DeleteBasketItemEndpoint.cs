using AuraShop.Shared.Extensions;
using MediatR;

namespace AuraShop.Basket.Features.Baskets.DeleteBasketItem
{
    public static class DeleteBasketItemEndpoint
    {
        public static RouteGroupBuilder DeleteBasketItemGroupEndpoint(this RouteGroupBuilder group)
        {
            group.MapDelete("/item/{productId:guid}", async (Guid productId, IMediator mediator) =>
                {
                    var result = await mediator.Send(new DeleteBasketItemCommand(productId));
                    return result.ToResult();

                }).WithName("DeleteBasketItem")
                .MapToApiVersion(1, 0);

            return group;
        }
    }
}
