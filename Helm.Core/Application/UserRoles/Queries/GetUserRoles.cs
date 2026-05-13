using Helm.Core.Application.Common;
using Helm.Core.Infrastructure.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Helm.Core.Application.UserRoles.Queries
{
    public record GetUserRolesQuery : IRequest<GetOperationResult<List<UserRoleDTO>>>;
    public class GetUserRolesQueryHandler : IRequestHandler<GetUserRolesQuery, GetOperationResult<List<UserRoleDTO>>>
    {
        private PostgresUserRoleRepository postgresUserRoleRepository;
        public GetUserRolesQueryHandler(PostgresUserRoleRepository repository)
        {
            postgresUserRoleRepository = repository;
        }
        public async Task<GetOperationResult<List<UserRoleDTO>>> Handle(GetUserRolesQuery request, CancellationToken cancellationToken)
        {

            List<UserRoleDTO> vm = await postgresUserRoleRepository.GetAllUserRolesAsync(cancellationToken);
            return new GetOperationResult<List<UserRoleDTO>>.Success(vm);
        }
    }

}
