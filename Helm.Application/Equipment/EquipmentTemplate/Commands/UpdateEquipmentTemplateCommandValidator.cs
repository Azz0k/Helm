using FluentValidation;
using Helm.Domain.Constants;
using System;
using System.Collections.Generic;
using System.Text;

namespace Helm.Application.Equipment.EquipmentTemplate.Commands
{
    public class UpdateEquipmentTemplateCommandValidator : AbstractValidator<UpdateEquipmentTemplateCommand>
    {
        public UpdateEquipmentTemplateCommandValidator()
        {
            RuleFor(x => x.Id)
                .Must(i => i > 0);
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(EquipmentTemplateConstants.NameMaxLength)
                .Must(str => str == str.Trim())
                .When(v => v.Description != null);
            RuleFor(x => x.Description)
                .NotEmpty()
                .MaximumLength(EquipmentTemplateConstants.DesciptionMaxLength)
                .Must(str => str == str.Trim())
                .When(v => v.Description != null);
        }
    }
}
