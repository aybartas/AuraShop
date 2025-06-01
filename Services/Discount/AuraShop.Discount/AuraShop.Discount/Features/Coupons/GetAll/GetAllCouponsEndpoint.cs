using AuraShop.Shared.Extensions;
using MediatR;

namespace AuraShop.Discount.Features.Coupons.GetAll
{
    public static class GetAllCouponsEndpoint
    {
        public static RouteGroupBuilder GetAllCoupons(this RouteGroupBuilder group)
        {
            group.MapGet("/", async (IMediator mediator) =>
            {
                var result = await mediator.Send(new GetAllCouponsQuery());

                return result.ToResult();

            }).WithName("GetAllCoupons").MapToApiVersion(1, 0);

            return group;
        }
    }
}
