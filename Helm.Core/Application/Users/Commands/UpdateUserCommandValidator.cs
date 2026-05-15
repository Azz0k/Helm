using FluentValidation;
using Helm.Core.Domain.Constants;
using System;
using System.Collections.Generic;
using System.Text;

namespace Helm.Core.Application.Users.Commands
{
    public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserCommandValidator() 
        {
            RuleFor(v => v.Id)
                .Must(id => id > 0);
            RuleFor(v => v.Login)
                .NotNull()
                .NotEmpty()
                .MaximumLength(UserConstants.LoginMaxLength)
                .Must(str => str == str.Trim());
            RuleFor(v => v.Name)
                .NotNull()
                .NotEmpty()
                .MaximumLength(UserConstants.NameMaxLength)
                .Must(str => str == str.Trim());
            RuleFor(v => v.ADLogin)
               .MaximumLength(UserConstants.ADLoginMaxLength)
               .When(v => v.ADLogin != null);

        }
    }
}
