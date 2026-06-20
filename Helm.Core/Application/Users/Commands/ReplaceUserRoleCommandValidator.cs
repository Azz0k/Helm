using FluentValidation;
using FluentValidation.Validators;
using System;
using System.Collections.Generic;
using System.Text;

namespace Helm.Core.Application.Users.Commands
{
    public class ReplaceUserRoleCommandValidator : AbstractValidator<ReplaceUserRoleCommand>
    {
        public ReplaceUserRoleCommandValidator()
        {
            RuleFor(x => x.UserId)
                .Must(id => id > 0);
            RuleFor(v => v.Roles)
                .NotNull()
                .ForEach(role =>
                {
                    role.Must(r => r > 0);
                });
        }
    }
}
