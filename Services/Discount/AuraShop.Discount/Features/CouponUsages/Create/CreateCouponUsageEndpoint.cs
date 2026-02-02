using AuraShop.Shared.Extensions;
using AuraShop.Shared.Filters;
using MediatR;

namespace AuraShop.Discount.Features.CouponUsages.Create
{
    public static class CreateCouponUsageEndpoint
    {
        public static RouteGroupBuilder CreateCouponUsage(this RouteGroupBuilder group)
        {
            group.MapPost("/", async (CreateCouponUsageCommand command, IMediator mediator) =>
            {
                var result = await mediator.Send(command);

                return result.ToResult();

            }).WithName("CreateCouponUsage").MapToApiVersion(1,0).AddEndpointFilter<ValidationFilter<CreateCouponUsageCommand>>();

            return group;
        }
    }
}
