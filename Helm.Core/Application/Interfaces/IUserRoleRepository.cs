using Helm.Core.Application.UserRoles.Queries;
using Helm.Core.Application.Users.Queries;
using Helm.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Helm.Core.Application.Interfaces
{
    public interface IUserRoleRepository
    {
        Task<UserRolesVm> GetAllUserRolesAsync(CancellationToken cancellationToken);
        Task<UserRolesVm> CreateRoleAsync(UserRole role, CancellationToken cancellationToken);
        Task<Boolean> FindByNameAsync(string name, CancellationToken cancellationToken);
        Task<Boolean> DeleteByIdAsync(int Id, CancellationToken cancellationToken);
        Task<Boolean> FindByIdAsync(int Id, CancellationToken cancellationToken);
        Task<UserRolesVm?> UpdateRoleAsync(UserRole role, CancellationToken cancellationToken);
    }
}
