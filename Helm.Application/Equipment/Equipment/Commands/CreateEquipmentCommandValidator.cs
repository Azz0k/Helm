using FluentValidation;
using Helm.Domain.Constants;


namespace Helm.Application.Equipment.Equipment.Commands
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
