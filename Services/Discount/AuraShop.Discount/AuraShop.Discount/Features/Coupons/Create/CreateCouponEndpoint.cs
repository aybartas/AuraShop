using AuraShop.Shared.Extensions;
using AuraShop.Shared.Filters;
using MediatR;

namespace AuraShop.Discount.Features.Coupons.Create
{
    public static class CreateCouponUsageEndpoint
    {
        public static RouteGroupBuilder CreateCoupon(this RouteGroupBuilder group)
        {
            group.MapPost("/", async (CreateCouponCommand command, IMediator mediator) =>
            {
                var result = await mediator.Send(command);

                return result.ToResult();

            }).WithName("CreateCoupon").MapToApiVersion(1,0).AddEndpointFilter<ValidationFilter<CreateCouponCommand>>();

            return group;
        }
    }
}
