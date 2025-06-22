using AuraShop.Basket.Features.Baskets.DeleteBasketItem;
using FluentValidation;

namespace AuraShop.Basket.Features.Baskets.UpdateBasketItem;

public class UpdateBasketItemCommandValidator : AbstractValidator<DeleteBasketItemCommand>
{
    public UpdateBasketItemCommandValidator()
    {
        RuleFor(x => x.ProductId)
            .NotEmpty().WithMessage("ProductId is required.");
    }
}