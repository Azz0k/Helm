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
    public record CreateUserRoleCommand: IRequest<GetOperationResult<UserRolesVm>>
    {
        public required string Name { get; set; }
        public string? Description { get; set; }
    }
    public class CreateUserRoleHandler : IRequestHandler<CreateUserRoleCommand, GetOperationResult<UserRolesVm>>
    {
        private PostgresUserRoleRepository postgresUserRoleRepository;
        private readonly IValidator<CreateUserRoleCommand> validator;
        public CreateUserRoleHandler(PostgresUserRoleRepository postgresUserRoleRepository, IValidator<CreateUserRoleCommand> validator)
        {
            this.postgresUserRoleRepository = postgresUserRoleRepository;
            this.validator = validator;
        }
        public async Task<GetOperationResult<UserRolesVm>> Handle(CreateUserRoleCommand command, CancellationToken cancellationToken)
        {
            var validationResult = validator.Validate(command);
            if (!validationResult.IsValid)
            {
                return new GetOperationResult<UserRolesVm>.Invalid();
            }
            if (await postgresUserRoleRepository.FindByNameAsync(command.Name, cancellationToken))
            {
                return new GetOperationResult<UserRolesVm>.Conflict();
            }
            var entity = new UserRole
            {
                Name = command.Name,
                Description = command.Description,
            };
            UserRolesVm vm = await postgresUserRoleRepository.CreateRoleAsync(entity, cancellationToken);
            return new GetOperationResult<UserRolesVm>.Success(vm);
        }
    }
}
