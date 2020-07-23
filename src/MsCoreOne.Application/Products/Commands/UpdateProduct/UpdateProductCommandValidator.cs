using FluentValidation;

namespace MsCoreOne.Application.Products.Commands.UpdateProduct
{
    public class UpdateProductCommandValidator : AbstractValidator<UpdateProductDto>
    {
        public UpdateProductCommandValidator()
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
        }
    }
}
