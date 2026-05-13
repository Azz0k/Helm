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
        Task<UserRoleDTO> CreateRoleAsync(UserRole role, CancellationToken cancellationToken);
        Task<Boolean> FindByNameAsync(string name, CancellationToken cancellationToken);
        Task<Boolean> DeleteByIdAsync(int Id, CancellationToken cancellationToken);
        Task<Boolean> FindByIdAsync(int Id, CancellationToken cancellationToken);
        Task<UserRoleDTO?> UpdateRoleAsync(UserRole role, CancellationToken cancellationToken);
    }
}
