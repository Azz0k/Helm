using FluentValidation;
using Helm.Domain.Constants;
using System;
using System.Collections.Generic;
using System.Text;

namespace Helm.Application.Equipment.Equipment.Commands
{
    public class ReturnEquipmentCommandValidator : AbstractValidator<ReturnEquipmentCommand>
    {
        public ReturnEquipmentCommandValidator()
        {
            RuleFor(x => x.Id)
                .Must(id => id > 0);
        }
    }
}
