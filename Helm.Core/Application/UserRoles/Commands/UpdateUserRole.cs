using AutoMapper;
using FluentValidation;
using Helm.Core.Application.Common;
using Helm.Core.Application.Interfaces;
using Helm.Core.Application.UserRoles.Queries;
using Helm.Core.Domain.Entities;
using Helm.Core.Infrastructure.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Helm.Core.Application.UserRoles.Commands
{
    public class UpdateUserRole
    {
        [RequireRole("UserRoleManager")]
        public record UpdateUserRoleCommand : IRequest<GetOperationResult<UserRoleDTO>>
        {
            public required int Id { get; set; }
            public required string Name { get; set; }
            public string? Description { get; set; }
        }
        public class UpdateUserRoleHandler : IRequestHandler<UpdateUserRoleCommand, GetOperationResult<UserRoleDTO>>
        {
            private IUserRoleRepository userRoleRepository;
            private IMapper mapper;
            public UpdateUserRoleHandler(IUserRoleRepository userRoleRepository, IMapper mapper)
            {
                this.mapper = mapper;   
                this.userRoleRepository = userRoleRepository;
            }
            public async Task<GetOperationResult<UserRoleDTO>> Handle(UpdateUserRoleCommand command, CancellationToken cancellationToken)
            {
                UserRole? currentRole = await userRoleRepository.FindByIdAsync(command.Id);
                if (currentRole == null)
                {
                    return new GetOperationResult<UserRoleDTO>.NotFound();
                }
                if (command.Name != currentRole.Name)
                {
                    UserRole? targetRole = await userRoleRepository.FindByNameAsync(command.Name, cancellationToken);
                    if (targetRole != null)
                    {
                        return new GetOperationResult<UserRoleDTO>.Conflict();
                    }
                    currentRole.Rename(command.Name);
                }
                if (command.Description != null)
                {
                    currentRole.ChangeDescription(command.Description);
                }
                await userRoleRepository.SaveChangesAsync(cancellationToken);
                UserRoleDTO dto = mapper.Map<UserRoleDTO>(currentRole);
                return new GetOperationResult<UserRoleDTO>.Success(dto);
            }
        }
    }
}
