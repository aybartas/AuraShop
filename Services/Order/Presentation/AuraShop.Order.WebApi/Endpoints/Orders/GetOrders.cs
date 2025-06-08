using AuraShop.Order.Application.Features.Order.GetUserOrders;
using AuraShop.Shared.Extensions;
using MediatR;

namespace AuraShop.Order.API.Endpoints.Orders
{
    public static class GetOrders
    {
        public static RouteGroupBuilder AddGetOrdersEndpoint(this RouteGroupBuilder group)
        {
            group.MapGet("/", async (IMediator mediator) =>
                {
                    var result = await mediator.Send(new GetUserOrdersQuery());

                    return result.ToResult();

                })
                .WithName("GetOrders")
                .MapToApiVersion(1, 0);
            return group;
        }
    }
}
