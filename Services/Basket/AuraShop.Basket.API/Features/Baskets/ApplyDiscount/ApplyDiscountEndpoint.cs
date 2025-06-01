using AuraShop.Shared.Extensions;
using AuraShop.Shared.Filters;
using MediatR;

namespace AuraShop.Basket.API.Features.Baskets.ApplyDiscount;

public static class ApplyDiscountEndpoint
{
    public static RouteGroupBuilder ApplyCouponToBasketEndpoint(this RouteGroupBuilder group)
    {
        group.MapPut("/apply-discount", async (ApplyCouponCommand command, IMediator mediator) =>
            {
                var result = await mediator.Send(command);

                return result.ToResult();

            }).WithName("ApplyDiscount")
            .MapToApiVersion(1, 0)
            .AddEndpointFilter<ValidationFilter<ApplyCouponCommand>>();

        return group;
    }
}