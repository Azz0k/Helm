using Helm.Core.Application.Common;
using Helm.Core.Infrastructure.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Helm.Core.Application.UserRoles.Queries
{
    public record GetUserRolesQuery : IRequest<GetOperationResult<UserRolesVm>>;
    public class GetUserRolesQueryHandler : IRequestHandler<GetUserRolesQuery, GetOperationResult<UserRolesVm>>
    {
        private PostgresUserRoleRepository postgresUserRoleRepository;
        public GetUserRolesQueryHandler(PostgresUserRoleRepository repository)
        {
            postgresUserRoleRepository = repository;
        }
        public async Task<GetOperationResult<UserRolesVm>> Handle(GetUserRolesQuery request, CancellationToken cancellationToken)
        {

            UserRolesVm vm = await postgresUserRoleRepository.GetAllUserRolesAsync(cancellationToken);
            return new GetOperationResult<UserRolesVm>.Success(vm);
        }
    }

}
