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
            private readonly IValidator<UpdateUserRoleCommand> validator;
            public UpdateUserRoleHandler(IUserRoleRepository userRoleRepository, IValidator<UpdateUserRoleCommand> validator)
            {
                this.userRoleRepository = userRoleRepository;
                this.validator = validator;
            }
            public async Task<GetOperationResult<UserRoleDTO>> Handle(UpdateUserRoleCommand command, CancellationToken cancellationToken)
            {
                var entity = new UserRole
                {
                    Id = command.Id,
                    Name = command.Name,
                    Description = command.Description,
                };
                UserRoleDTO? vm = await userRoleRepository.UpdateRoleAsync(entity, cancellationToken);
                if (vm == null)
                {
                    return new GetOperationResult<UserRoleDTO>.NotFound();
                }
                return new GetOperationResult<UserRoleDTO>.Success(vm);
            }
        }
    }
}
