using FluentValidation;
using Helm.Core.Application.Common;
using Helm.Core.Application.UserRoles.Queries;
using Helm.Core.Domain.Entities;
using Helm.Core.Infrastructure.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Helm.Core.Application.UserRoles.Commands
{
    public record DeleteUserRoleCommand : IRequest<GetOperationResult<object>>
    {
        public required int Id { get; set; }
    }
    public class DeleteUserRole : IRequestHandler<DeleteUserRoleCommand, GetOperationResult<object>>
    {
        private PostgresUserRoleRepository postgresUserRoleRepository;
        private readonly IValidator<DeleteUserRoleCommand> validator;
        public DeleteUserRole(PostgresUserRoleRepository postgresUserRoleRepository, IValidator<DeleteUserRoleCommand> validator)
        {
            this.postgresUserRoleRepository = postgresUserRoleRepository;
            this.validator = validator;
        }
        public async Task<GetOperationResult<object>> Handle(DeleteUserRoleCommand command, CancellationToken cancellationToken)
        {
            if (await postgresUserRoleRepository.DeleteByIdAsync(command.Id, cancellationToken))
            {
                return new GetOperationResult<object>.Success(new Object());
            }
            return new GetOperationResult<object>.NotFound();
        }
    }
}
