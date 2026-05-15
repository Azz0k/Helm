using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Helm.Core.Application.Users.Commands
{
    public class AssignUserRoleCommandValidator : AbstractValidator<AssignUserRoleCommand>
    {
        public AssignUserRoleCommandValidator()
        {
            RuleFor(x => x.RoleId)
                .Must(id => id > 0);
            RuleFor(x => x.UserId)
                .Must(id => id > 0);
        }
    }
}
