using AuraShop.Order.Application.Features.Order.Create;
using AuraShop.Shared.Extensions;
using AuraShop.Shared.Filters;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AuraShop.Order.WebApi.Endpoints.Orders
{
    public static class CreateOrder
    {
        public static RouteGroupBuilder AddOrderGroupEndpointExt(this RouteGroupBuilder group)
        {
            group.MapPost("/", async ([FromBody] CreateOrderCommand command, IMediator mediator) =>
                {
                    var result = await mediator.Send(command);

                    return result.ToResult();

                })
                .WithName("CreateOrder")
                .MapToApiVersion(1, 0)
                .AddEndpointFilter<ValidationFilter<CreateOrderCommand>>();

            return group;
        }
    }
}
