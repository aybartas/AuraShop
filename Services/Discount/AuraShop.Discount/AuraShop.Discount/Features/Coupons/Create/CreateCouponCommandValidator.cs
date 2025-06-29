using FluentValidation;

namespace AuraShop.Discount.Features.Coupons.Create
{
    public partial class CreateCouponUsageCommandValidator : AbstractValidator<CreateCouponCommand>
    {
        public CreateCouponUsageCommandValidator()
        {
            RuleFor(x => x.Code)
                .NotEmpty()
                .WithMessage("Code is required");

            RuleFor(x => x.Rate)
                .GreaterThan(0)
                .WithMessage("Discount rate should be greater than zero");

            RuleFor(x => x.ExpirationDate)
                .NotEmpty()
                .WithMessage("Expiration date is required");
        }
    }
}
