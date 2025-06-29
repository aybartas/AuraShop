using AuraShop.Shared.Extensions;
using MediatR;

namespace AuraShop.Discount.Features.Coupons.Validate
{
    public static class ValidateCouponEndpoint
    {
        public static RouteGroupBuilder ValidateCoupon(this RouteGroupBuilder group)
        {
            group.MapGet("/{code}/validate", async (string code,IMediator mediator) =>
            {
                var result = await mediator.Send(new ValidateCouponQuery(){Code = code});

                return result.ToResult();

            }).WithName("ValidateByCode").MapToApiVersion(1, 0);

            return group;
        }
    }
}
