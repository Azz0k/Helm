using Helm.Application.Equipment.Equipment.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using FluentValidation;
using FluentValidation.TestHelper;
using Helm.Domain.Constants;
using Helm.Application.Equipment.EquipmentTemplate.Commands;


namespace Helm.Tests.ValidatorTests
{
    public class EquipmentTemplateValidatorsTests
    {
        private readonly CreateEquipmentTemplateCommandValidator createEquipmentTemplateCommandValidator = new();
        private readonly UpdateEquipmentTemplateCommandValidator updateEquipmentTemplateCommandValidator = new();
        private CreateEquipmentTemplateCommand GenerateValidCreateEquipmentTemplateCommand()
        {
            return new CreateEquipmentTemplateCommand()
            {
                Name = new string('a', EquipmentTemplateConstants.NameMaxLength),
                Description = new string('a', EquipmentTemplateConstants.DesciptionMaxLength),
                RenderTemplateKey = new string('a', EquipmentTemplateConstants.KeyMaxLength),
            };
        }
        private UpdateEquipmentTemplateCommand GenerateValidUpdateEquipmentTemplateCommand()
        {
            return new UpdateEquipmentTemplateCommand()
            {
                Id = 1,
                Name = new string('a', EquipmentTemplateConstants.NameMaxLength),
                Description = new string('a', EquipmentTemplateConstants.DesciptionMaxLength),
                Enabled = true,
            };
        }
        [Theory]
        [InlineData(" ")]
        [InlineData("")]
        [InlineData("\t")]
        [InlineData("\r")]
        [InlineData("\n")]
        [InlineData("Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aenean commodo ligula eget dolor. Aenean massa. Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Donec quam felis, ultricies nec, pellentesque eu, pretium quis, sem. Nulla consequat massa quis enim. Donec pede justo, fringilla vel, aliquet nec, vulputate eget, arcu. In enim justo, rhoncus ut, imperdiet a, venenatis vitae, justo. Nullam dictum felis eu pede mollis pretium. Integer tincidunt. Cras dapibus. Vivamus elementum semper nisi. Aenean vulputate eleifend tellus. Aenean leo ligula, porttitor eu, consequat vitae, eleifend ac, enim. Aliquam lorem ante, dapibus in, viverra quis, feugiat a, tellus.")]//too long
        [InlineData(" test ")]
        [InlineData(" test")]
        [InlineData("test ")]
        public void NameMustBeInvalid_CreateEquipmentTemplateCommand(string name)
        {
            var command = GenerateValidCreateEquipmentTemplateCommand();
            command.Name = name;
            var result = createEquipmentTemplateCommandValidator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Name);
        }
        [Theory]
        [InlineData(" ")]
        [InlineData("")]
        [InlineData("\t")]
        [InlineData("\r")]
        [InlineData("\n")]
        [InlineData("Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aenean commodo ligula eget dolor. Aenean massa. Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Donec quam felis, ultricies nec, pellentesque eu, pretium quis, sem. Nulla consequat massa quis enim. Donec pede justo, fringilla vel, aliquet nec, vulputate eget, arcu. In enim justo, rhoncus ut, imperdiet a, venenatis vitae, justo. Nullam dictum felis eu pede mollis pretium. Integer tincidunt. Cras dapibus. Vivamus elementum semper nisi. Aenean vulputate eleifend tellus. Aenean leo ligula, porttitor eu, consequat vitae, eleifend ac, enim. Aliquam lorem ante, dapibus in, viverra quis, feugiat a, tellus.")]//too long
        [InlineData(" test ")]
        [InlineData(" test")]
        [InlineData("test ")]
        public void DescriptionMustBeInvalid_CreateEquipmentTemplateCommand(string description)
        {
            var command = GenerateValidCreateEquipmentTemplateCommand();
            command.Description = description;
            var result = createEquipmentTemplateCommandValidator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Description);
        }
        [Theory]
        [InlineData(" ")]
        [InlineData("")]
        [InlineData("\t")]
        [InlineData("\r")]
        [InlineData("\n")]
        [InlineData("Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aenean commodo ligula eget dolor. Aenean massa. Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Donec quam felis, ultricies nec, pellentesque eu, pretium quis, sem. Nulla consequat massa quis enim. Donec pede justo, fringilla vel, aliquet nec, vulputate eget,")]//too long
        [InlineData(" test ")]
        [InlineData(" test")]
        [InlineData("test ")]
        public void KeyMustBeInvalid_CreateEquipmentTemplateCommand(string key)
        {
            var command = GenerateValidCreateEquipmentTemplateCommand();
            command.RenderTemplateKey = key;
            var result = createEquipmentTemplateCommandValidator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.RenderTemplateKey);
        }
        [Theory]
        [InlineData(" ")]
        [InlineData("")]
        [InlineData("\t")]
        [InlineData("\r")]
        [InlineData("\n")]
        [InlineData("Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aenean commodo ligula eget dolor. Aenean massa. Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Donec quam felis, ultricies nec, pellentesque eu, pretium quis, sem. Nulla consequat massa quis enim. Donec pede justo, fringilla vel, aliquet nec, vulputate eget, arcu. In enim justo, rhoncus ut, imperdiet a, venenatis vitae, justo. Nullam dictum felis eu pede mollis pretium. Integer tincidunt. Cras dapibus. Vivamus elementum semper nisi. Aenean vulputate eleifend tellus. Aenean leo ligula, porttitor eu, consequat vitae, eleifend ac, enim. Aliquam lorem ante, dapibus in, viverra quis, feugiat a, tellus.")]//too long
        [InlineData(" test ")]
        [InlineData(" test")]
        [InlineData("test ")]
        public void NameMustBeInvalid_UpdateEquipmentTemplateCommand(string name)
        {
            var command = GenerateValidUpdateEquipmentTemplateCommand();
            command.Name = name;
            var result = updateEquipmentTemplateCommandValidator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Name);
        }
        [Theory]
        [InlineData(" ")]
        [InlineData("")]
        [InlineData("\t")]
        [InlineData("\r")]
        [InlineData("\n")]
        [InlineData("Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aenean commodo ligula eget dolor. Aenean massa. Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Donec quam felis, ultricies nec, pellentesque eu, pretium quis, sem. Nulla consequat massa quis enim. Donec pede justo, fringilla vel, aliquet nec, vulputate eget, arcu. In enim justo, rhoncus ut, imperdiet a, venenatis vitae, justo. Nullam dictum felis eu pede mollis pretium. Integer tincidunt. Cras dapibus. Vivamus elementum semper nisi. Aenean vulputate eleifend tellus. Aenean leo ligula, porttitor eu, consequat vitae, eleifend ac, enim. Aliquam lorem ante, dapibus in, viverra quis, feugiat a, tellus.")]//too long
        [InlineData(" test ")]
        [InlineData(" test")]
        [InlineData("test ")]
        public void DescriptionMustBeInvalid_UpdateEquipmentTemplateCommand(string description)
        {
            var command = GenerateValidUpdateEquipmentTemplateCommand();
            command.Description = description;
            var result = updateEquipmentTemplateCommandValidator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Description);
        }
    }
}
