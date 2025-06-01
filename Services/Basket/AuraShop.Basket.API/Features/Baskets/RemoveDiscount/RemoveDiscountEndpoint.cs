using AuraShop.Shared.Extensions;
using MediatR;

namespace AuraShop.Basket.API.Features.Baskets.RemoveDiscount;

public static class RemoveDiscountEndpoint
{
    public static RouteGroupBuilder RemoveDiscountGroupEndpoint(this RouteGroupBuilder group)
    {
        group.MapPut("/remove-discount", async (IMediator mediator) =>
            {
                var result = await mediator.Send(new RemoveDiscountCommand());

                return result.ToResult();

            }).WithName("RemoveDiscount")
            .MapToApiVersion(1, 0);

        return group;
    }
}