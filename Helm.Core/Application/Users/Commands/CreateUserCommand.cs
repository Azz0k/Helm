using AutoMapper;
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
    }
    public class CreateUserHandler : IRequestHandler<CreateUserCommand, GetOperationResult<UserDTO>>
    {
        private IUserRepository userRepository;
        private IUserRoleRepository userRoleRepository;
        private IMapper mapper;
        public CreateUserHandler(IUserRepository userRepository, IUserRoleRepository userRoleRepository, IMapper mapper)  
        {
            this.userRepository = userRepository;
            this.userRoleRepository = userRoleRepository;
            this.mapper = mapper;
        }

        public async Task<GetOperationResult<UserDTO>> Handle(CreateUserCommand command, CancellationToken cancellationToken)
        {
            command.Login = command.Login.ToLower();
            User? user = await userRepository.FindUserByLoginAsync(command.Login, cancellationToken);
            if (user != null)
            {
                return new GetOperationResult<UserDTO>.Conflict();
            }
            User newUser = new(command.Login, command.Name);
            await userRepository.AddUserAsync(newUser);
            await userRepository.SaveChangesAsync(cancellationToken);
            UserDTO dto = mapper.Map<UserDTO>(newUser);
            return new GetOperationResult<UserDTO>.Success(dto);
        }
    }
}
