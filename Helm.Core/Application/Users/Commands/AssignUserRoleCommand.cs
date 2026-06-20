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
    [RequireRole("UserManager")]
    public record AssignUserRoleCommand : IRequest<GetOperationResult<UserDTO>>
    {
        public int UserId { get; set; }
        public int RoleId { get; set; }
    }
    public class AssignUserRoleHandler : IRequestHandler<AssignUserRoleCommand, GetOperationResult<UserDTO>>
    {
        private IUserRepository userRepository;
        private IUserRoleRepository roleRepository;
        private IValidator<AssignUserRoleCommand> validator;
        public AssignUserRoleHandler(IUserRepository userRepository, IUserRoleRepository roleRepository, IValidator<AssignUserRoleCommand> validator)
        {
            this.userRepository = userRepository;
            this.roleRepository = roleRepository;
            this.validator = validator;
        }
        public async Task<GetOperationResult<UserDTO>> Handle(AssignUserRoleCommand request, CancellationToken cancellationToken)
        {
            User? user = await userRepository.FindUserByIdAysnc(request.UserId, cancellationToken);
            if (user == null)
            {
                return new GetOperationResult<UserDTO>.NotFound();
            }
            if (user.HasRole(request.RoleId))
            {
                return new GetOperationResult<UserDTO>.Conflict();
            }
            UserRole? userRole = await roleRepository.FindByIdAsync(request.RoleId, cancellationToken);
            if (userRole == null)
            {
                return new GetOperationResult<UserDTO>.Invalid();
            }
            var dto = await userRepository.AssignUserRoleAsync(user, userRole, cancellationToken);
            return new GetOperationResult<UserDTO>.Success(dto);   

        }
    }
}
