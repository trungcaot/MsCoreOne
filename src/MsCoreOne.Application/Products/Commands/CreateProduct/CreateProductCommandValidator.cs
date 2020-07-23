using FluentValidation;

namespace MsCoreOne.Application.Products.Commands.CreateProduct
{
    public class CreateProductCommandValidator : AbstractValidator<CreateProductDto>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(v => v.Name)
                .MaximumLength(500)
                .NotEmpty();

            RuleFor(p => p.Price)
                .NotNull()
                .GreaterThan(0);

            RuleFor(p => p.BrandId)
                .NotNull()
                .GreaterThan(0);

            RuleFor(p => p.CategoryId)
                .NotNull()
                .GreaterThan(0);
        }
    }
}
