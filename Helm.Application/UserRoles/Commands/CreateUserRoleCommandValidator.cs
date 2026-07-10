using FluentValidation;
using Helm.Domain.Constants;

namespace Helm.Application.UserRoles.Commands
{
    public class CreateUserRoleCommandValidator : AbstractValidator<CreateUserRoleCommand>
    {
        public CreateUserRoleCommandValidator()
        {
            RuleFor(v => v.Name)
                .NotEmpty()
                .MaximumLength(UserRoleConstants.NameMaxLength)
                .Must(str => str==str.Trim());

        }
    }
}
