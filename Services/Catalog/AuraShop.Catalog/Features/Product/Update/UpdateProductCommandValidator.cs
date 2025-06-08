using FluentValidation;

namespace AuraShop.Catalog.Features.Product.Update
{
    public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Name is required");

            RuleFor(x => x.Price)
                .NotEmpty()
                .WithMessage("Price is required");

            RuleFor(x => x.Category)
                .NotEmpty()
                .WithMessage("Category is required");

            RuleFor(x => x.CategoryId)
                .NotEmpty()
                .WithMessage("Category id is required");
        }
    }
    
}
