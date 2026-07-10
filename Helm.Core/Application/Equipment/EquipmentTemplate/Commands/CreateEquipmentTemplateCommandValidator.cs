using FluentValidation;
using Helm.Domain.Constants;
using System;
using System.Collections.Generic;
using System.Text;

namespace Helm.Core.Application.Equipment.EquipmentTemplate.Commands
{
    public class CreateEquipmentTemplateCommandValidator : AbstractValidator<CreateEquipmentTemplateCommand>
    {
        public CreateEquipmentTemplateCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(EquipmentTemplateConstants.NameMaxLength)
                .Must(str => str == str.Trim());
            RuleFor(x => x.Description)
                .NotEmpty()
                .MaximumLength(EquipmentTemplateConstants.DesciptionMaxLength)
                .Must(str => str == str.Trim())
                .When(v => v.Description != null);
            RuleFor(x => x.RenderTemplateKey)
                .NotEmpty()
                .MaximumLength(EquipmentTemplateConstants.KeyMaxLength)
                .Must(str => str == str.Trim());
        }
    }
}
