using FluentValidation;

namespace MsCoreOne.Application.Users.Commands.UpdateUser
{
    public class UpdateUserCommandValidator : AbstractValidator<UpdateUserDto>
    {
        public UpdateUserCommandValidator()
        {
            RuleFor(v => v.Id)
                .NotEmpty();

            RuleFor(v => v.FirstName)
                .MaximumLength(100)
                .NotEmpty();

            RuleFor(v => v.LastName)
                .MaximumLength(100)
                .NotEmpty();

            RuleFor(v => v.UserName)
                .MaximumLength(100)
                .NotEmpty();
        }
    }
}
