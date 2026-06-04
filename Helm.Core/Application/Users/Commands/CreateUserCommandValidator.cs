using FluentValidation;
using Helm.Core.Domain.Constants;
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
            RuleFor(v => v.Password)
                .NotEmpty()
                .MinimumLength(UserConstants.PasswordMinLength)
                .MaximumLength(UserConstants.PasswordMaxLength)
                .When(v => v.Password != null);
            RuleFor(v => v.ADLogin)
                .NotEmpty()
                .Must(str => str == str.Trim())
                .MaximumLength(UserConstants.ADLoginMaxLength)
                .When(v => v.ADLogin != null);
            RuleFor(v => v.Roles)
                .NotNull()
                .ForEach(role =>
                {
                    role.Must(r => r>0);
                });
                
                
        }
    }
}
