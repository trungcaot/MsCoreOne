using FluentValidation;
using MsCoreOne.Application.Common.Interfaces;

namespace MsCoreOne.Application.Categories.Commands.UpdateCategory
{
    public class UpdateCategoryCommandValidatorValidator : AbstractValidator<UpdateCategoryDto>
    {
        public UpdateCategoryCommandValidatorValidator(IApplicationDbContext context)
        {
            RuleFor(v => v.Name)
                .MaximumLength(500)
                .NotEmpty();

            RuleFor(x => x)
                .MustAsync(async (dto, cancellationToken) =>
                    await new DataConflictValidator(dto, context).ValidateDataConflictAsync());
        }
    }
}
