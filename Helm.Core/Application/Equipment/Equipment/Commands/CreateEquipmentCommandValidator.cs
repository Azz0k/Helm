using FluentValidation;
using Helm.Core.Domain.Constants;


namespace Helm.Core.Application.Equipment.Equipment.Commands
{
    public class CreateEquipmentCommandValidator : AbstractValidator<CreateEquipmentCommand>
    {
        public CreateEquipmentCommandValidator()
        {
            RuleFor(v => v.Name)
                .NotEmpty()
                .MaximumLength(EquipmentConstants.NameMaxLength)
                .Must(str => str == str.Trim());
        }
    }
}
