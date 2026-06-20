using FluentValidation;
using Helm.Core.Application.Common;
using Helm.Core.Application.Interfaces;
using Helm.Core.Application.UserRoles.Commands;
using Helm.Core.Application.UserRoles.Queries;
using Helm.Core.Application.Users.Queries;
using Helm.Core.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Helm.Core.Application.Users.Commands
{
    [RequireRole("UserManager")]
    public record CreateUserCommand : IRequest<GetOperationResult<UserDTO>>
    {
        public required string Login { get; set; }
        public required string Name { get; set; }
        public string? Password { get; set; }
        public string? ADLogin { get; set; }
        public bool Enabled { get; set; }
        public List<int> Roles { get; set; } = [];
    }
    public class CreateUserHandler : IRequestHandler<CreateUserCommand, GetOperationResult<UserDTO>>
    {
        private IUserRepository userRepository;
        private IUserRoleRepository userRoleRepository;
        private readonly IValidator<CreateUserCommand> validator;
        public CreateUserHandler(IUserRepository userRepository, IUserRoleRepository userRoleRepository, IValidator<CreateUserCommand> validator)
        {
            this.userRepository = userRepository;
            this.userRoleRepository = userRoleRepository;
            this.validator = validator;
        }

        public async Task<GetOperationResult<UserDTO>> Handle(CreateUserCommand command, CancellationToken cancellationToken)
        {
            User? user = await userRepository.FindUserByLoginAsync(command.Login, cancellationToken);
            if (user != null)
            {
                return new GetOperationResult<UserDTO>.Conflict();
            }
            User newUser = new() { Login =  command.Login, Name = command.Name, ADLogin = command.ADLogin, Enabled = command.Enabled};
            if (command.Password != null)
            {
                newUser.Hash = BCrypt.Net.BCrypt.HashPassword(command.Password);
            }
            foreach (var roleId in command.Roles)
            {
                UserRole? role = await userRoleRepository.FindByIdAsync(roleId, cancellationToken);
                if (role == null)
                {
                    return new GetOperationResult<UserDTO>.Invalid();
                }
                newUser.Roles.Add(role);
            }
            UserDTO dto = await userRepository.AddUserAsync(newUser, cancellationToken);
            return new GetOperationResult<UserDTO>.Success(dto);
        }
    }
}
