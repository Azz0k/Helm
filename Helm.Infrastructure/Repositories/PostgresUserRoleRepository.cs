using AutoMapper;
using AutoMapper.QueryableExtensions;
using Helm.Application.Interfaces;
using Helm.Application.UserRoles.Queries;
using Helm.Application.Users.Queries;
using Helm.Domain.Entities;
using Helm.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Security;
using System.Text;

namespace Helm.Infrastructure.Repositories
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
        public async Task AddRoleAsync(UserRole role)
        {
            await dBContext.UserRoles.AddAsync(role);
        }
        public async Task SaveChangesAsync(CancellationToken cancellationToken)
        {
            await dBContext.SaveChangesAsync(cancellationToken);
        }
        public async Task<UserRole?> FindByNameAsync(string name, CancellationToken cancellationToken)
        {
            return await dBContext.UserRoles.FirstOrDefaultAsync(role=>role.Name==name, cancellationToken);
        }
        public async Task<UserRole?> FindByIdAsync(int Id)
        {
            return await dBContext.UserRoles.FindAsync(Id);
        }
        public async Task<Boolean> DeleteByIdAsync(int Id, CancellationToken cancellationToken)
        {
            UserRole? existingRole = await dBContext.UserRoles.FindAsync(Id);
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
