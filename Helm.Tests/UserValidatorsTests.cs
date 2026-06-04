using FluentValidation;
using FluentValidation.TestHelper;
using Helm.Core.Application.UserRoles.Commands;
using Helm.Core.Application.Users.Commands;
using Helm.Core.Domain.Constants;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Helm.Tests
{
    public class UserValidatorsTests
    {
        private CreateUserCommandValidator createUserCommandValidator = new();
        private AssignUserRoleCommandValidator assignUserRoleCommandValidator = new();
        private DeleteUserCommandValidator deleteUserCommandValidator = new();
        private RemoveUserRoleCommandValidator removeUserRoleCommandValidator = new();
        private UpdateUserCommandValidator updateUserCommandValidator = new();
        private UpdateUserPasswordCommandValidator updateUserPasswordCommandValidator = new();
        private UpdateUserStatusCommandValidator updateUserStatusCommandValidator = new();
        public UserValidatorsTests() { }
        private CreateUserCommand GenerateValidCreateUserCommand()
        {
            return new CreateUserCommand() { 
                Login = new string('a', UserConstants.LoginMaxLength),
                ADLogin = new string('a', UserConstants.LoginMaxLength),
                Name = new string('a', UserConstants.NameMaxLength),
                Password = new string('a', UserConstants.PasswordMaxLength),
                Roles = [1, 2, 3]
            };
        }
        private AssignUserRoleCommand GenerateValidAssignUserRoleCommand()
        {
            return new AssignUserRoleCommand()
            {
                RoleId = 1,
                UserId = 1
            };
        }
        private DeleteUserCommand GenerateValidDeleteUserRoleCommand()
        {
            return new DeleteUserCommand()
            {
                Id = 1,
            };
        }
        private RemoveUserRoleCommand GenerateValidRemoveUserRoleCommand()
        {
            return new RemoveUserRoleCommand()
            {
                RoleId = 1,
                UserId = 1
            };
        }
        private UpdateUserCommand GenerateValidUpdateUserCommand()
        {
            return new UpdateUserCommand()
            {
                Login = new string('a', UserConstants.LoginMaxLength),
                ADLogin = new string('a', UserConstants.LoginMaxLength),
                Name = new string('a', UserConstants.NameMaxLength),
                Id = 1,
            };
        }
        private UpdateUserPasswordCommand GenerateValidUpdateUserPasswordCommand()
        {
            return new UpdateUserPasswordCommand()
            {
                Id = 1,
                Password = new string('a', UserConstants.PasswordMaxLength),
            };
        }
        private UpdateUserStatusCommand GenerateValidUpdateUserStatusCommand()
        {
            return new UpdateUserStatusCommand()
            {
                Id = 1,
                Enabled = true,
            };
        }
        [Fact]
        public void HappyPath_CreateUserCommand()
        {
            var command = GenerateValidCreateUserCommand();
            var result = createUserCommandValidator.TestValidate(command);
            result.ShouldNotHaveAnyValidationErrors();
        }
        
        [Theory]
        [InlineData(" ")]
        [InlineData("")]
        [InlineData("\t")]
        [InlineData("\r")]
        [InlineData("\n")]
        [InlineData("Lorem1ipsum2dolor3sit4amet56consectetuer7adipiscing")]//too long
        [InlineData(" test ")]
        [InlineData(" test")]
        [InlineData("test ")]
        public void LoginMustBeInvalid_CreateUserCommand(string login)
        {
            var command = GenerateValidCreateUserCommand();
            command.Login = login;
            var result = createUserCommandValidator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x=>x.Login);
        }
 
        [Theory]
        [InlineData(" ")]
        [InlineData("")]
        [InlineData("\t")]
        [InlineData("\r")]
        [InlineData("\n")]
        [InlineData("Lorem1ipsum2dolor3sit4amet56consectetuer7adipiscingLorem1ipsum2dolor3sit4amet56consectetuer7adipiscin")]//too long
        [InlineData(" test ")]
        [InlineData(" test")]
        [InlineData("test ")]
        public void NameMustBeInvalid_CreateUserCommand(string name)
        {
            var command = GenerateValidCreateUserCommand();
            command.Name = name;
            var result = createUserCommandValidator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Name);
        }
        [Theory]
        [InlineData(" ")]
        [InlineData("")]
        [InlineData("\t")]
        [InlineData("\r")]
        [InlineData("\n")]
        [InlineData("Lorem1ipsum2dolor3sit4amet56consectetuer7adipiscingLorem1ipsum2dolor3sit4amet56consectetuer7adipiscinLorem1ipsum2dolor3sit4amet56consectetuer7adipiscingLorem1ipsum2dolor3sit4amet56consectetuer7adipisci")]//too long
        [InlineData(" test ")]
        [InlineData(" test")]
        [InlineData("test ")]
        public void ADLoginMustBeInvalid_CreateUserCommand(string adlogin)
        {
            var command = GenerateValidCreateUserCommand();
            command.ADLogin = adlogin;
            var result = createUserCommandValidator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.ADLogin);
        }
        [Theory]
        [InlineData("")]
        [InlineData("Lorem1ipsum2dolor3sit4amet")]//too long
        [InlineData("1234567")]//too short
        public void PasswordMustBeInvalid_CreateUserCommand(string password)
        {
            var command = GenerateValidCreateUserCommand();
            command.Password = password;
            var result = createUserCommandValidator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Password);
        }
        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void RolesMustContainOnlyPositiveIds_CreateUserCommand(int roleId)
        {
            var command = GenerateValidCreateUserCommand();
            command.Roles.Add(roleId);
            var result = createUserCommandValidator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Roles);
        }
        [Fact]
        public void HappyPath_AssignUserRoleCommand()
        {
            var command = GenerateValidAssignUserRoleCommand();
            var result = assignUserRoleCommandValidator.TestValidate(command);
            result.ShouldNotHaveAnyValidationErrors();
        }
        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void RolesMustContainOnlyPositiveIds_AssignUserRoleCommand(int roleId)
        {
            var command = GenerateValidAssignUserRoleCommand();
            command.RoleId = roleId;
            var result = assignUserRoleCommandValidator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.RoleId);
        }
        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void UsersMustContainOnlyPositiveIds_AssignUserRoleCommand(int userId)
        {
            var command = GenerateValidAssignUserRoleCommand();
            command.UserId = userId;
            var result = assignUserRoleCommandValidator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.UserId);
        }
        
        [Fact]
        public void HappyPath_DeleteUserRoleCommand()
        {
            var command = GenerateValidDeleteUserRoleCommand();
            var result = deleteUserCommandValidator.TestValidate(command);
            result.ShouldNotHaveAnyValidationErrors();
        }
        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void UsersMustContainOnlyPositiveIds_DeleteUserRoleCommand(int userId)
        {
            var command = GenerateValidDeleteUserRoleCommand();
            command.Id = userId;
            var result = deleteUserCommandValidator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Id);
        }
        [Fact]
        public void HappyPath_RemoveUserRoleCommand()
        {
            var command = GenerateValidRemoveUserRoleCommand();
            var result = removeUserRoleCommandValidator.TestValidate(command);
            result.ShouldNotHaveAnyValidationErrors();
        }
        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void RolesMustContainOnlyPositiveIds_RemoveUserRoleCommand(int roleId)
        {
            var command = GenerateValidRemoveUserRoleCommand();
            command.RoleId = roleId;
            var result = removeUserRoleCommandValidator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.RoleId);
        }
        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void UsersMustContainOnlyPositiveIds_RemoveUserRoleCommand(int userId)
        {
            var command = GenerateValidRemoveUserRoleCommand();
            command.UserId = userId;
            var result = removeUserRoleCommandValidator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.UserId);
        }
        [Fact]
        public void HappyPath_UpdateUserCommand()
        {
            var command = GenerateValidUpdateUserCommand();
            var result = updateUserCommandValidator.TestValidate(command);
            result.ShouldNotHaveAnyValidationErrors();
        }

        [Theory]
        [InlineData(" ")]
        [InlineData("")]
        [InlineData("\t")]
        [InlineData("\r")]
        [InlineData("\n")]
        [InlineData("Lorem1ipsum2dolor3sit4amet56consectetuer7adipiscing")]//too long
        [InlineData(" test ")]
        [InlineData(" test")]
        [InlineData("test ")]
        public void LoginMustBeInvalid_UpdateUserCommand(string login)
        {
            var command = GenerateValidUpdateUserCommand();
            command.Login = login;
            var result = updateUserCommandValidator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Login);
        }

        [Theory]
        [InlineData(" ")]
        [InlineData("")]
        [InlineData("\t")]
        [InlineData("\r")]
        [InlineData("\n")]
        [InlineData("Lorem1ipsum2dolor3sit4amet56consectetuer7adipiscingLorem1ipsum2dolor3sit4amet56consectetuer7adipiscin")]//too long
        [InlineData(" test ")]
        [InlineData(" test")]
        [InlineData("test ")]
        public void NameMustBeInvalid_UpdateUserCommand(string name)
        {
            var command = GenerateValidUpdateUserCommand();
            command.Name = name;
            var result = updateUserCommandValidator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Name);
        }
        [Theory]
        [InlineData(" ")]
        [InlineData("")]
        [InlineData("\t")]
        [InlineData("\r")]
        [InlineData("\n")]
        [InlineData("Lorem1ipsum2dolor3sit4amet56consectetuer7adipiscingLorem1ipsum2dolor3sit4amet56consectetuer7adipiscinLorem1ipsum2dolor3sit4amet56consectetuer7adipiscingLorem1ipsum2dolor3sit4amet56consectetuer7adipisci")]//too long
        [InlineData(" test ")]
        [InlineData(" test")]
        [InlineData("test ")]
        public void ADLoginMustBeInvalid_UpdateUserCommand(string adlogin)
        {
            var command = GenerateValidUpdateUserCommand();
            command.ADLogin = adlogin;
            var result = updateUserCommandValidator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.ADLogin);
        }
        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void UsersMustContainOnlyPositiveIds_UpdateUserCommand(int userId)
        {
            var command = GenerateValidUpdateUserCommand();
            command.Id = userId;
            var result = updateUserCommandValidator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Id);
        }
        [Fact]
        public void HappyPath_UpdateUserPasswordCommand()
        {
            var command = GenerateValidUpdateUserPasswordCommand();
            var result = updateUserPasswordCommandValidator.TestValidate(command);
            result.ShouldNotHaveAnyValidationErrors();
        }
        [Theory]
        [InlineData("")]
        [InlineData("Lorem1ipsum2dolor3sit4amet")]//too long
        [InlineData("1234567")]//too short
        public void PasswordMustBeInvalid_UpdateUserPasswordCommand(string password)
        {
            var command = GenerateValidUpdateUserPasswordCommand();
            command.Password = password;
            var result = updateUserPasswordCommandValidator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Password);
        }
        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void UsersMustContainOnlyPositiveIds_UpdateUserPasswordCommand(int userId)
        {
            var command = GenerateValidUpdateUserPasswordCommand();
            command.Id = userId;
            var result = updateUserPasswordCommandValidator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Id);
        }
        [Fact]
        public void HappyPath_UpdateUserStatusCommand()
        {
            var command = GenerateValidUpdateUserStatusCommand();
            var result = updateUserStatusCommandValidator.TestValidate(command);
            result.ShouldNotHaveAnyValidationErrors();
        }
        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void UsersMustContainOnlyPositiveIds_UpdateUserStatusCommand(int userId)
        {
            var command = GenerateValidUpdateUserStatusCommand();
            command.Id = userId;
            var result = updateUserStatusCommandValidator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Id);
        }
    }
}
