using AuraShop.Shared.Extensions;
using MediatR;

namespace AuraShop.Discount.Features.Coupons.GetByCode
{
    public static class GetByCodeEndpoint
    {
        public static RouteGroupBuilder GetAllCoupons(this RouteGroupBuilder group)
        {
            group.MapGet("/{code}", async (string code,IMediator mediator) =>
            {
                var result = await mediator.Send(new GetByCodeQuery(){Code = code});

                return result.ToResult();

            }).WithName("GetCouponByCode").MapToApiVersion(1, 0);

            return group;
        }
    }
}
