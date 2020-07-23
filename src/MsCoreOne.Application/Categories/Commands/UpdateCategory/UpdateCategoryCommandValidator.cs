using FluentValidation;

namespace MsCoreOne.Application.Categories.Commands.UpdateCategory
{
    public class UpdateCategoryCommandValidatorValidator : AbstractValidator<UpdateCategoryDto>
    {
        public UpdateCategoryCommandValidatorValidator()
        {
            RuleFor(v => v.Name)
                .MaximumLength(500)
                .NotEmpty();
        }
    }
}
