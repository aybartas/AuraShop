using FluentValidation;

namespace AuraShop.Basket.API.Features.Baskets.ApplyDiscount;

public class ApplyDiscountCommandValidator : AbstractValidator<ApplyCouponCommand>
{
    public ApplyDiscountCommandValidator()
    {
        RuleFor(x => x.Coupon)
            .NotEmpty().WithMessage("Coupon is required.");

        RuleFor(x => x.DiscountRate)
            .NotEmpty().GreaterThan(0).WithMessage("Discount rate is required");
    }
}