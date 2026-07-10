using Helm.Application.UserRoles.Queries;
using System;
using System.Collections.Generic;
using System.Text;

namespace Helm.Application.Interfaces
{
    public interface IAuthorizationService
    {
        Task<CurrentUserRolesDTO> GetCurrentUserRolesAsync(string? login, CancellationToken cancellationToken);
        Task<bool> IsAllowedAsync(string? login, System.Type? type, CancellationToken cancellationToken);
    }
}
