using AutoMapper;
using AutoMapper.QueryableExtensions;
using Helm.Core.Application.Interfaces;
using Helm.Core.Application.UserRoles.Queries;
using Helm.Core.Application.Users.Queries;
using Helm.Core.Domain.Entities;
using Helm.Core.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Security;
using System.Text;

namespace Helm.Core.Infrastructure.Repositories
{
    public class PostgresUserRoleRepository: IUserRoleRepository
    {
        private PostgresDBContext dBContext;
        private IMapper mapper;
        public PostgresUserRoleRepository(PostgresDBContext dBContext, IMapper mapper)
        {
            this.dBContext = dBContext;
            this.mapper = mapper;
        }

        public async Task<List<UserRoleDTO>> GetAllUserRolesAsync(CancellationToken cancellationToken)
        {
            return await dBContext.UserRoles
                .AsNoTracking()
                .ProjectTo<UserRoleDTO>(mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
        }
        public async Task<UserRoleDTO> AddRoleAsync(UserRole role, CancellationToken cancellationToken)
        {
            await dBContext.UserRoles.AddAsync(role);
            await dBContext.SaveChangesAsync(cancellationToken);
            return mapper.Map<UserRoleDTO>(role);
        }
        public async Task<UserRoleDTO?> UpdateRoleAsync(UserRole role, CancellationToken cancellationToken)
        {
            var currentRole = await dBContext.UserRoles.FindAsync(role.Id,cancellationToken);
            if (currentRole == null)
            {
                return null;
            }
            currentRole.Description = role.Description;
            currentRole.Name = role.Name;
            await dBContext.SaveChangesAsync(cancellationToken);
            return mapper.Map<UserRoleDTO>(role);
        }
        public async Task<Boolean> FindByNameAsync(string name, CancellationToken cancellationToken)
        {
            UserRole? existingRole = await dBContext.UserRoles.FirstOrDefaultAsync(role=>role.Name==name, cancellationToken);
            if (existingRole == null)
            {
                return false;
            }
            return true;
        }
        public async Task<UserRole?> FindByIdAsync(int Id, CancellationToken cancellationToken)
        {
            return await dBContext.UserRoles.FindAsync(Id, cancellationToken);
        }
        public async Task<Boolean> DeleteByIdAsync(int Id, CancellationToken cancellationToken)
        {
            UserRole? existingRole = await dBContext.UserRoles.FindAsync(Id, cancellationToken);
            if (existingRole == null)
            {
                return false;
            }
            dBContext.UserRoles.Remove(existingRole);
            await dBContext.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
