using AuraShop.Shared;
using MediatR;

namespace AuraShop.Payment.Features.Payments.CreatePayment;

public class CreatePaymentCommand : IRequest<ServiceResult<CreatePaymentResult>>
{
    public decimal Amount { get; set; }
    public string Currency { get; set; } = "usd";
    public string? StripeToken { get; set; }
    public string? Description { get; set; }
}
