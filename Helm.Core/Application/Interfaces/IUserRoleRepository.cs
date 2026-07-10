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
        Task<List<UserRoleDTO>> GetAllUserRolesAsync(CancellationToken cancellationToken);
        Task AddRoleAsync(UserRole role);
        Task<UserRole?> FindByNameAsync(string name, CancellationToken cancellationToken);
        Task<Boolean> DeleteByIdAsync(int Id, CancellationToken cancellationToken);
        Task<UserRole?> FindByIdAsync(int Id);
        Task SaveChangesAsync(CancellationToken cancellationToken);
    }
}
