using AutoMapper;
using FluentValidation;
using Helm.Core.Application.Common;
using Helm.Core.Application.Interfaces;
using Helm.Core.Application.Users.Queries;
using Helm.Domain.Entities;
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
        private IMapper mapper;
        public AssignUserRoleHandler(IUserRepository userRepository, IUserRoleRepository roleRepository, IMapper mapper)
        {
            this.userRepository = userRepository;
            this.roleRepository = roleRepository;
            this.mapper = mapper;
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
            UserRole? userRole = await roleRepository.FindByIdAsync(request.RoleId);
            if (userRole == null)
            {
                return new GetOperationResult<UserDTO>.Invalid();
            }
            user.AddRole(userRole);
            await userRepository.SaveChangesAsync(cancellationToken);
            var dto = mapper.Map<UserDTO>(user);
            return new GetOperationResult<UserDTO>.Success(dto);   

        }
    }
}
