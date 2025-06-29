using FluentValidation;

namespace AuraShop.Basket.Features.Baskets.ApplyDiscount;

public class ApplyDiscountCommandValidator : AbstractValidator<ApplyCouponCommand>
{
    public ApplyDiscountCommandValidator()
    {
        RuleFor(x => x.CouponCode)
            .NotEmpty().WithMessage("Coupon code is required.");
    }
}