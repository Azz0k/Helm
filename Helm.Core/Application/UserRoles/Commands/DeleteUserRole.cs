using FluentValidation;
using Helm.Core.Application.Common;
using Helm.Core.Application.Interfaces;
using Helm.Core.Application.UserRoles.Queries;
using Helm.Domain.Entities;
using Helm.Core.Infrastructure.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Helm.Core.Application.UserRoles.Commands
{
    [RequireRole("UserRoleManager")]
    public record DeleteUserRoleCommand : IRequest<GetOperationResult<UserRoleDTO>>
    {
        public required int Id { get; set; }
    }
    public class DeleteUserRole : IRequestHandler<DeleteUserRoleCommand, GetOperationResult<UserRoleDTO>>
    {
        private IUserRoleRepository userRoleRepository;
        public DeleteUserRole(IUserRoleRepository userRoleRepository)
        {
            this.userRoleRepository = userRoleRepository;
        }
        public async Task<GetOperationResult<UserRoleDTO>> Handle(DeleteUserRoleCommand command, CancellationToken cancellationToken)
        {
            if (await userRoleRepository.DeleteByIdAsync(command.Id, cancellationToken))
            {
                return new GetOperationResult<UserRoleDTO>.Success(null);
            }
            return new GetOperationResult<UserRoleDTO>.NotFound();
        }
    }
}
