using AuraShop.Payment.Database;
using AuraShop.Shared;
using AuraShop.Shared.Services;
using MediatR;
using Stripe;

namespace AuraShop.Payment.Features.Payments.CreatePayment;

public class CreatePaymentHandler(PaymentDbContext context, IIdentityService identityService) : IRequestHandler<CreatePaymentCommand, ServiceResult<CreatePaymentResult>>
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

        var status = charge.Status == "succeeded" ? PaymentStatus.Success : PaymentStatus.Failed;

        var payment = Payment.Create(charge.Id, status, identityService.UserId.Value, request.OrderNumber, request.Amount);

        context.Payments.Add(payment);

        await context.SaveChangesAsync(cancellationToken);

        if (status == PaymentStatus.Failed)
            return ServiceResult<CreatePaymentResult>.Error("Payment Failed", "The payment process failed. Please try again.", System.Net.HttpStatusCode.BadRequest);
        
        return ServiceResult<CreatePaymentResult>.SuccessAsCreated(new CreatePaymentResult
        {
            Success = charge.Status == "succeeded",
            PaymentId = charge.Id,
            Message = charge.Status
        }, $"/api/payments/{payment.Id}");
    }
}
