using AuraShop.Catalog.Features.Product.Update;
using FluentValidation;

namespace AuraShop.Catalog.Features.Product.Create
{
    public class CreateProductCommandValidator : AbstractValidator<UpdateProductCommand>
    {
        public CreateProductCommandValidator()
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
