using Helm.Core.Application.UserRoles.Commands;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Runtime.Remoting;
using System.Text;
using System.Xml.Linq;
using Xunit;
using static Helm.Core.Application.UserRoles.Commands.UpdateUserRole;

namespace Helm.Tests
{
    public class UserRolesValidatorsTests
    {
        public UserRolesValidatorsTests() { }

        public static TheoryData<CreateUserRoleCommand, bool> CreateUserRoleCommands = new TheoryData<CreateUserRoleCommand, bool>()
        {
            {new CreateUserRoleCommand(){ Name = "test" }, true },
            {new CreateUserRoleCommand(){ Name = "test " }, false },
            {new CreateUserRoleCommand(){ Name = " test" }, false },
            {new CreateUserRoleCommand(){ Name = " test " }, false },
            {new CreateUserRoleCommand(){ Name = " " }, false },
            {new CreateUserRoleCommand(){ Name = "" }, false },
            {new CreateUserRoleCommand(){ Name = "\t" }, false },
            {new CreateUserRoleCommand(){ Name = "\n" }, false },
            {new CreateUserRoleCommand(){ Name = new string('a', 51) }, false },
            {new CreateUserRoleCommand(){ Name = new string('a', 50) }, true },
        };
        [Theory]
        [MemberData(nameof(CreateUserRoleCommands))]
        public void CreateUserRoleCommandValidator_ShouldReturnExpectedValidationResult(CreateUserRoleCommand command, bool expected)
        {
            var validator = new CreateUserRoleCommandValidator();
            var result = validator.Validate(command);
            Assert.Equal(expected, result.IsValid);
        }
        public static TheoryData<UpdateUserRoleCommand, bool> UpdateUserRoleCommands = new TheoryData<UpdateUserRoleCommand, bool>()
        {
            {new UpdateUserRoleCommand(){ Id = 1, Name = "test" }, true },
            {new UpdateUserRoleCommand(){ Id = 1, Name = "test " }, false },
            {new UpdateUserRoleCommand(){ Id = 1, Name = " test" }, false },
            {new UpdateUserRoleCommand(){ Id = 1, Name = " test " }, false },
            {new UpdateUserRoleCommand(){ Id = 1, Name = " " }, false },
            {new UpdateUserRoleCommand(){ Id = 1, Name = "" }, false },
            {new UpdateUserRoleCommand(){ Id = 1, Name = "\t" }, false },
            {new UpdateUserRoleCommand(){ Id = 1, Name = "\n" }, false },
            {new UpdateUserRoleCommand(){ Id = 1, Name = new string('a', 50) }, true },
            {new UpdateUserRoleCommand(){ Id = 1, Name = new string('a', 51) }, false },
            {new UpdateUserRoleCommand(){ Id = 0, Name = "test" }, false },
            {new UpdateUserRoleCommand(){ Id = -1, Name = "test" }, false },
        };
        [Theory]
        [MemberData(nameof(UpdateUserRoleCommands))]
        public void UpdateUserRoleCommandValidator_ShouldReturnExpectedValidationResult(UpdateUserRoleCommand command, bool expected)
        {
            var validator = new UpdateUserRoleCommandValidator();
            var result = validator.Validate(command);
            Assert.Equal(expected, result.IsValid);
        }
        public static TheoryData<DeleteUserRoleCommand, bool> DeleteUserRoleCommands = new TheoryData<DeleteUserRoleCommand, bool>()
        {
            {new DeleteUserRoleCommand(){ Id = 1 }, true },
            {new DeleteUserRoleCommand(){ Id = 0 }, false },
            {new DeleteUserRoleCommand(){ Id = -1 }, false },
        };
        [Theory]
        [MemberData(nameof(DeleteUserRoleCommands))]
        public void DeleteUserRoleCommandValidator_ShouldReturnExpectedValidationResult(DeleteUserRoleCommand command, bool expected)
        {
            var validator = new DeleteUserRoleCommandValidator();
            var result = validator.Validate(command);
            Assert.Equal(expected, result.IsValid);
        }
    }
}
