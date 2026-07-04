using FluentValidation;
using Helm.Core.Domain.Constants;
using System;
using System.Collections.Generic;
using System.Text;

namespace Helm.Core.Application.Equipment.Equipment.Commands
{
    public class LoseEquipmentCommandValidator : AbstractValidator<LoseEquipmentCommand>
    {
        public LoseEquipmentCommandValidator()
        {
            RuleFor(x => x.Id)
                .Must(id => id > 0);
        }
    }
}
