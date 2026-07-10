using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Helm.Application.Users.Commands
{
    public class UpdateUserStatusCommandValidator : AbstractValidator<UpdateUserStatusCommand>
    {
        public UpdateUserStatusCommandValidator()
        {
            RuleFor(v => v.Id)
                .Must(id => id > 0);
        }
    }
}
