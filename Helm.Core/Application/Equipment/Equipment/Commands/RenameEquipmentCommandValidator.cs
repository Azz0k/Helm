using FluentValidation;
using Helm.Core.Domain.Constants;
using System;
using System.Collections.Generic;
using System.Text;

namespace Helm.Core.Application.Equipment.Equipment.Commands
{
    public class RenameEquipmentCommandValidator : AbstractValidator<RenameEquipmentCommand>
    {
        public RenameEquipmentCommandValidator()
        {
            RuleFor(x => x.Id)
                .Must(id => id > 0);
            RuleFor(v => v.Name)
                .NotEmpty()
                .MaximumLength(EquipmentConstants.NameMaxLength)
                .Must(str => str == str.Trim());
        }
    }
}
