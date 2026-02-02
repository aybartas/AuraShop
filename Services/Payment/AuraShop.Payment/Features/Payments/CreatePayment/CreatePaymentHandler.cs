using AuraShop.Shared;
using MediatR;
using Stripe;

namespace AuraShop.Payment.Features.Payments.CreatePayment;

public class CreatePaymentHandler : IRequestHandler<CreatePaymentCommand, ServiceResult<CreatePaymentResult>>
{
    public async Task<ServiceResult<CreatePaymentResult>> Handle(CreatePaymentCommand request, CancellationToken cancellationToken)
    {
        var options = new ChargeCreateOptions
        {
            Amount = (long)(request.Amount * 100),
            Currency = request.Currency,
            Source = request.StripeToken,
            Description = request.Description,
        };
        var service = new ChargeService();
        var charge = await service.CreateAsync(options, cancellationToken: cancellationToken);

        //TODO : Write this to database, 
        // write to order

        return ServiceResult<CreatePaymentResult>.SuccessAsCreated(new CreatePaymentResult
            {
                Success = charge.Status == "succeeded",
                PaymentId = charge.Id,
                Message = charge.Status
            }, $"/api/payments/{charge.Id}");
    }
}
