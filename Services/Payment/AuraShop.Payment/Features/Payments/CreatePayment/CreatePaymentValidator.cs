using FluentValidation;

namespace AuraShop.Payment.Features.Payments.CreatePayment;

public class CreatePaymentValidator : AbstractValidator<CreatePaymentCommand>
{
    public CreatePaymentValidator()
    {
        RuleFor(x => x.Amount).GreaterThan(0);
    }
}
