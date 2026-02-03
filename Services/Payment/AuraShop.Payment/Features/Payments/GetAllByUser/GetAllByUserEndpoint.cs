using AuraShop.Shared.Extensions;
using MediatR;

namespace AuraShop.Payment.Features.Payments.GetAllByUser;

public static class GetAllByUserEndpoint
{
    public static RouteGroupBuilder GetAllByUser(this RouteGroupBuilder group)
    {
        group.MapGet("/user", async (IMediator mediator) =>
        {
            var result = await mediator.Send(new GetAllByUserQuery());
            return result.ToResult();
        })
        .WithName("GetAllPaymentsByUser");

        return group;
    }
}