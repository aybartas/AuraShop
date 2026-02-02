using AuraShop.Shared.Extensions;
using AuraShop.Shared.Filters;
using MediatR;

namespace AuraShop.Payment.Features.Payments.CreatePayment;

public static class CreatePaymentEndpoint
{
    public static RouteGroupBuilder CreatePayment(this RouteGroupBuilder group)
    {
        group.MapPost("/", async (CreatePaymentCommand command, IMediator mediator) =>
        {
            var result = await mediator.Send(command);
            return result.ToResult();
        })
        .WithName("CreatePayment")
        .MapToApiVersion(1, 0).AddEndpointFilter<ValidationFilter<CreatePaymentCommand>>(); ;

        return group;

    }
}
