using Helm.Core.Application.Equipment.Equipment.Commands;
using FluentValidation;
using FluentValidation.TestHelper;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Helm.Domain.Constants;

namespace Helm.Tests.ValidatorTests
{
    public class EquipmentValidatorTests
    {
        private CreateEquipmentCommandValidator createEquipmentCommandValidator = new();
        private RenameEquipmentCommandValidator renameEquipmentCommandValidator = new();
        private IssueEquipmentCommandValidator issueEquipmentCommandValidator = new();
        private ReturnEquipmentCommandValidator returnEquipmentCommandValidator = new();
        private LoseEquipmentCommandValidator loseEquipmentCommandValidator = new();
        private CreateEquipmentCommand GenerateValidCreateEquipmentCommand()
        {
            return new CreateEquipmentCommand()
            {
                Name = new string('a', EquipmentConstants.NameMaxLength)
            };
        }
        private RenameEquipmentCommand GenerateValidRenameEquipmentCommand()
        {
            return new RenameEquipmentCommand() { Id = 1, Name = new string('a', EquipmentConstants.NameMaxLength) };
        }
        private IssueEquipmentCommand GenerateValidIssueEquipmentCommand()
        {
            return new IssueEquipmentCommand() { Id = 1, IssuedBy = new string('a', EquipmentConstants.IssuedByMaxLength) };
        }
        private ReturnEquipmentCommand GenerateValidReturnEquipmentCommand()
        {
            return new ReturnEquipmentCommand() { Id = 1 };
        }
        private LoseEquipmentCommand GenerateValidLoseEquipmentCommand()
        {
            return new LoseEquipmentCommand() { Id = 1 };
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
        public void NameMustBeInvalid_RenameEquipmentCommand(string name)
        {
            var command = GenerateValidRenameEquipmentCommand();
            command.Name = name;
            var result = renameEquipmentCommandValidator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Name);
        }
        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void IdMustBeInvalid_RenameEquipmentCommand(int id)
        {
            var command = GenerateValidRenameEquipmentCommand();
            command.Id = id;
            var result = renameEquipmentCommandValidator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Id);
        }
        [Theory]
        [InlineData(" ")]
        [InlineData("")]
        [InlineData("\t")]
        [InlineData("\r")]
        [InlineData("\n")]
        [InlineData("Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aenean commodo ligula eget dolor. Aenean ma")]//too long
        [InlineData(" test ")]
        [InlineData(" test")]
        [InlineData("test ")]
        public void IssuedByMustBeInvalid_IssueEquipmentCommand(string issuedBy)
        {
            var command = GenerateValidIssueEquipmentCommand();
            command.IssuedBy = issuedBy;
            var result = issueEquipmentCommandValidator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.IssuedBy);
        }
        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void IdMustBeInvalid_IssueEquipmentCommand(int id)
        {
            var command = GenerateValidIssueEquipmentCommand();
            command.Id = id;
            var result = issueEquipmentCommandValidator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Id);
        }
        [Theory] 
        [InlineData(0)]
        [InlineData(-1)]
        public void IdMustBeInvalid_ReturnEquipmentCommand(int id)
        {
            var command = GenerateValidReturnEquipmentCommand();
            command.Id = id;
            var result = returnEquipmentCommandValidator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Id);
        }
        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void IdMustBeInvalid_LoseEquipmentCommand(int id)
        {
            var command = GenerateValidLoseEquipmentCommand();
            command.Id = id;
            var result = loseEquipmentCommandValidator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Id);
        }
    }
}
