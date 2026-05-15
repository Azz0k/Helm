
using FluentValidation;
using Helm.Core.Application.Common;
using Helm.Core.Application.Interfaces;
using Helm.Core.Application.Users.Queries;
using Helm.Core.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Helm.Core.Application.Users.Commands
{
    public record RemoveUserRoleCommand : IRequest<GetOperationResult<UserDTO>>
    {
        public int UserId { get; set; }
        public int RoleId { get; set; }
    }
    public class RemoveUserRoleHandler : IRequestHandler<RemoveUserRoleCommand, GetOperationResult<UserDTO>>
    {
        private IUserRepository userRepository;
        private IUserRoleRepository roleRepository;
        private IValidator<RemoveUserRoleCommand> validator;
        public RemoveUserRoleHandler(IUserRepository userRepository, IUserRoleRepository roleRepository, IValidator<RemoveUserRoleCommand> validator)
        {
            this.userRepository = userRepository;
            this.roleRepository = roleRepository;
            this.validator = validator;
        }

        public async Task<GetOperationResult<UserDTO>> Handle(RemoveUserRoleCommand request, CancellationToken cancellationToken)
        {
            User? user = await userRepository.FindUserByIdAysnc(request.UserId, cancellationToken);
            if (user == null)
            {
                return new GetOperationResult<UserDTO>.NotFound();
            }
            if (!user.HasRole(request.RoleId))
            {
                return new GetOperationResult<UserDTO>.Invalid();
            }
            UserRole? userRole = await roleRepository.FindByIdAsync(request.RoleId, cancellationToken);
            if (userRole == null)
            {
                return new GetOperationResult<UserDTO>.Invalid();
            }
            UserDTO? dto = await userRepository.RemoveUserRoleAsync(user, userRole, cancellationToken);
            if (dto == null)
            {
                return new GetOperationResult<UserDTO>.Conflict();
            }
            return new GetOperationResult<UserDTO>.Success(dto);
        }
    }
}
