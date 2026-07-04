using Helm.Core.Application.Equipment.Equipment.Commands;
using FluentValidation;
using FluentValidation.TestHelper;
using Helm.Core.Domain.Constants;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Helm.Tests
{
    public class EquipmentValidatorTests
    {
        private CreateEquipmentCommandValidator createEquipmentCommandValidator = new();
        private CreateEquipmentCommand GenerateValidCreateEquipmentCommand()
        {
            return new CreateEquipmentCommand()
            {
                Name = new string('a', EquipmentConstants.NameMaxLength)
            };
        }
        [Theory]
        [InlineData(" ")]
        [InlineData("")]
        [InlineData("\t")]
        [InlineData("\r")]
        [InlineData("\n")]
        [InlineData("Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aenean commodo ligula eget dolor. Aenean massa. Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Donec quam felis, ultricies nec, pellentesque eu, pretium quis, sem. Nulla consequat massa quis enim. Donec pede justo, fringilla vel, aliquet nec, vulputate eget, arcu. In enim justo, rhoncus ut, imperdiet a, venenatis vitae, justo. Nullam dictum felis eu pede mollis pretium. Integer tincidunt. Cras dapibus")]//too long
        [InlineData(" test ")]
        [InlineData(" test")]
        [InlineData("test ")]
        public void NameMustBeInvalid_CreateEquipmentCommand(string name)
        {
            var command = GenerateValidCreateEquipmentCommand();
            command.Name = name;
            var result = createEquipmentCommandValidator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Name);
        }
    }
}
