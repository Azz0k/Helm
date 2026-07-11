using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Helm.Application.Equipment.EquipmentTemplate.Commands
{
    public class DeleteEquipmentTemplateCommandValidator : AbstractValidator<DeleteEquipmentTemplateCommand>
    {
        public DeleteEquipmentTemplateCommandValidator()
        {
            RuleFor(x => x.Id)
                .Must(i => i > 0);
        }
    }
}
