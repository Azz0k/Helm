using Helm.Core.Application.Common;
using Helm.Core.Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Helm.Core.Application.UserRoles.Queries
{
    public record GetCurrentUserRolesQuery : IRequest<GetOperationResult<CurrentUserRolesDTO>>
    {
        public string? Login { get; init; }
    }
    public class GetCurrentUserRolesQueryHandler : IRequestHandler<GetCurrentUserRolesQuery, GetOperationResult<CurrentUserRolesDTO>>
    {
        private IAuthorizationService authorizationService;
        public GetCurrentUserRolesQueryHandler(IAuthorizationService authorizationService)
        {
            this.authorizationService = authorizationService;
        }
        public async Task<GetOperationResult<CurrentUserRolesDTO>> Handle(GetCurrentUserRolesQuery request, CancellationToken cancellationToken)
        {
            var vm = await authorizationService.GetCurrentUserRolesAsync(request.Login, cancellationToken);
            return new GetOperationResult<CurrentUserRolesDTO>.Success(vm);
        }
    }
}
