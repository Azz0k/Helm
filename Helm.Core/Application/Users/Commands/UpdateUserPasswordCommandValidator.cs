using FluentValidation;
using Helm.Core.Domain.Constants;
using System;
using System.Collections.Generic;
using System.Text;

namespace Helm.Core.Application.Users.Commands
{
    public class UpdateUserPasswordCommandValidator : AbstractValidator<UpdateUserPasswordCommand>
    {
        public UpdateUserPasswordCommandValidator()
        {
            RuleFor(v => v.Id)
                .Must(id => id > 0);
            RuleFor(v => v.Password)
                .NotEmpty()
                .MinimumLength(UserConstants.PasswordMinLength)
                .MaximumLength(UserConstants.PasswordMaxLength);
        }
    }
}
