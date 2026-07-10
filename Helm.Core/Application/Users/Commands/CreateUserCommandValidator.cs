using FluentValidation;
using Helm.Domain.Constants;
using System;
using System.Collections.Generic;
using System.Text;

namespace Helm.Core.Application.Users.Commands
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(v => v.Login)
                .NotNull()
                .NotEmpty()
                .MaximumLength(UserConstants.LoginMaxLength)
                .Must(str => str==str.Trim());
            RuleFor(v => v.Name)
                .NotNull()
                .NotEmpty()
                .MaximumLength(UserConstants.NameMaxLength)
                .Must(str => str==str.Trim());
        }
    }
}
