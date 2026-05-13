using FluentValidation;
using Helm.Core.Application.Common;
using Helm.Core.Application.UserRoles.Queries;
using Helm.Core.Domain.Entities;
using Helm.Core.Infrastructure.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Helm.Core.Application.UserRoles.Commands
{
    public record CreateUserRoleCommand: IRequest<GetOperationResult<UserRoleDTO>>
    {
        public required string Name { get; set; }
        public string? Description { get; set; }
    }
    public class CreateUserRoleHandler : IRequestHandler<CreateUserRoleCommand, GetOperationResult<UserRoleDTO>>
    {
        private PostgresUserRoleRepository postgresUserRoleRepository;
        private readonly IValidator<CreateUserRoleCommand> validator;
        public CreateUserRoleHandler(PostgresUserRoleRepository postgresUserRoleRepository, IValidator<CreateUserRoleCommand> validator)
        {
            this.postgresUserRoleRepository = postgresUserRoleRepository;
            this.validator = validator;
        }
        public async Task<GetOperationResult<UserRoleDTO>> Handle(CreateUserRoleCommand command, CancellationToken cancellationToken)
        {
            if (await postgresUserRoleRepository.FindByNameAsync(command.Name, cancellationToken))
            {
                return new GetOperationResult<UserRoleDTO>.Conflict();
            }
            var entity = new UserRole
            {
                Name = command.Name,
                Description = command.Description,
            };
            UserRoleDTO vm = await postgresUserRoleRepository.CreateRoleAsync(entity, cancellationToken);
            return new GetOperationResult<UserRoleDTO>.Success(vm);
        }
    }
}
