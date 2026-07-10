using FluentValidation;
using Helm.Domain.Constants;
using System;
using System.Collections.Generic;
using System.Text;
using static Helm.Application.UserRoles.Commands.UpdateUserRole;

namespace Helm.Application.UserRoles.Commands
{
    public class UpdateUserRoleCommandValidator : AbstractValidator<UpdateUserRoleCommand>
    {
        public UpdateUserRoleCommandValidator()
        {
            RuleFor(v => v.Name)
                .NotEmpty()
                .MaximumLength(UserRoleConstants.NameMaxLength)
                .Must(str => str==str.Trim());
            RuleFor(v => v.Id)
                .Must(id => id > 0);
        }
    }
}
