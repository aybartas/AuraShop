using AuraShop.Order.Application.Validators;
using FluentValidation;

namespace AuraShop.Order.Application.Features.Order.Create;

public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
{
    public CreateOrderCommandValidator()
    {
        RuleFor(x => x.ShippingAddress)
            .NotNull().WithMessage("Shipping address is required.")
            .SetValidator(new AddressDtoValidator());

        RuleFor(x => x.Items)
            .NotNull().WithMessage("Order items cannot be null.")
            .NotEmpty().WithMessage("At least one order item is required.");

        RuleForEach(x => x.Items).SetValidator(new OrderItemDtoValidator());
    }
}