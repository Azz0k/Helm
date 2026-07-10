using FluentValidation;
using Helm.Domain.Constants;
using System;
using System.Collections.Generic;
using System.Text;

namespace Helm.Application.Equipment.Equipment.Commands
{
    public class IssueEquipmentCommandValidator : AbstractValidator<IssueEquipmentCommand>
    {
        public IssueEquipmentCommandValidator()
        {
            RuleFor(x => x.Id)
                .Must(id => id > 0);
            RuleFor(v => v.IssuedBy)
                .NotEmpty()
                .MaximumLength(EquipmentConstants.IssuedByMaxLength)
                .Must(str => str == str.Trim());
        }
    }
}
