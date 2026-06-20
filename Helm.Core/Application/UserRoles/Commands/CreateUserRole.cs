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
    [RequireRole("UserRoleManager")]
    public record CreateUserRoleCommand: IRequest<GetOperationResult<UserRoleDTO>>
    {
        public required string Name { get; set; }
        public string? Description { get; set; }
    }
    public class CreateUserRoleHandler : IRequestHandler<CreateUserRoleCommand, GetOperationResult<UserRoleDTO>>
    {
        private IUserRoleRepository userRoleRepository;
        private readonly IValidator<CreateUserRoleCommand> validator;
        public CreateUserRoleHandler(IUserRoleRepository userRoleRepository, IValidator<CreateUserRoleCommand> validator)
        {
            this.userRoleRepository = userRoleRepository;
            this.validator = validator;
        }
        public async Task<GetOperationResult<UserRoleDTO>> Handle(CreateUserRoleCommand command, CancellationToken cancellationToken)
        {
            if (await userRoleRepository.FindByNameAsync(command.Name, cancellationToken))
            {
                return new GetOperationResult<UserRoleDTO>.Conflict();
            }
            var entity = new UserRole
            {
                Name = command.Name,
                Description = command.Description,
            };
            UserRoleDTO vm = await userRoleRepository.AddRoleAsync(entity, cancellationToken);
            return new GetOperationResult<UserRoleDTO>.Success(vm);
        }
    }
}
