using FluentValidation;
using Helm.Domain.Constants;
using System;
using System.Collections.Generic;
using System.Text;

namespace Helm.Application.Equipment.Equipment.Commands
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
