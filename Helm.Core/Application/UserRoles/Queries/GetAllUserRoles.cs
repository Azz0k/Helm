using Helm.Core.Application.Common;
using Helm.Core.Application.Interfaces;
using Helm.Core.Infrastructure.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Helm.Core.Application.UserRoles.Queries
{
    [RequireRole("UserRoleManager")]
    public record GetAllUserRolesQuery : IRequest<GetOperationResult<List<UserRoleDTO>>>;
    public class GetAllUserRolesQueryHandler : IRequestHandler<GetAllUserRolesQuery, GetOperationResult<List<UserRoleDTO>>>
    {
        private IUserRoleRepository userRoleRepository;
        public GetAllUserRolesQueryHandler(IUserRoleRepository userRoleRepository)
        {
            this.userRoleRepository = userRoleRepository;
        }
        public async Task<GetOperationResult<List<UserRoleDTO>>> Handle(GetAllUserRolesQuery request, CancellationToken cancellationToken)
        {

            List<UserRoleDTO> vm = await userRoleRepository.GetAllUserRolesAsync(cancellationToken);
            return new GetOperationResult<List<UserRoleDTO>>.Success(vm);
        }
    }

}
