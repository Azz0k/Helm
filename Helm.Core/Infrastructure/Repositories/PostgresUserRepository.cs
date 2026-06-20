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
using System.Data;
using System.Text;

namespace Helm.Core.Infrastructure.Repositories
{
    public class PostgresUserRepository : IUserRepository
    {
        private PostgresDBContext dBContext;
        private IMapper mapper;
        public PostgresUserRepository(PostgresDBContext dBContext, IMapper mapper) 
        {
            this.dBContext = dBContext;
            this.mapper = mapper;
        }
        public async Task<UserDTO> AddUserAsync(User user, CancellationToken cancellationToken)
        {
            await dBContext.Users.AddAsync(user);
            await dBContext.SaveChangesAsync(cancellationToken);
            return mapper.Map<UserDTO>(user);
        }

        public async Task<Boolean> DeleteUserAsync(int id, CancellationToken cancellationToken)
        {
            User? user = await dBContext.Users.FindAsync(id, cancellationToken);
            if (user == null)
            {
                return false;
            }
            dBContext.Users.Remove(user);
            await dBContext.SaveChangesAsync(cancellationToken);
            return true;
        }

        public async Task<List<UserDTO>> GetAllUsersAsync(CancellationToken cancellationToken)
        {
            return await dBContext.Users
                .AsNoTracking()
                .ProjectTo<UserDTO>(mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
        }

        public async Task<User?> FindUserByIdAysnc(int id, CancellationToken cancellationToken)
        {
            User? user = await dBContext.Users.Include(p => p.Roles).FirstOrDefaultAsync(u => u.Id == id, cancellationToken);
            return user;
        }

        public async Task<UserDTO> UpdateUserAsync(User user, CancellationToken cancellationToken)
        {
            await dBContext.SaveChangesAsync(cancellationToken);
            return mapper.Map<UserDTO>(user);
        }

        public async Task<User?> FindUserByLoginAsync(string login, CancellationToken cancellationToken)
        {
            User? user = await dBContext.Users.Include(p => p.Roles).FirstOrDefaultAsync(user => user.Login == login, cancellationToken);
            return user;
        }
        public async Task<UserDTO> AssignUserRoleAsync(User user, UserRole userRole, CancellationToken cancellationToken)
        {
            user.Roles.Add(userRole);
            await dBContext.SaveChangesAsync(cancellationToken);
            return mapper.Map<UserDTO>(user);
        }
        public async Task<UserDTO?> RemoveUserRoleAsync(User user, UserRole userRole, CancellationToken cancellationToken)
        {
            if (!user.Roles.Remove(userRole))
            {
                return null;
            }
            await dBContext.SaveChangesAsync(cancellationToken);
            return mapper.Map<UserDTO>(user);
        }

        public async Task<UserDTO> ReplaceUserRoleAsync(User user, List<int> roles, CancellationToken cancellationToken)
        {
            user.Roles = [];
            foreach (int roleId in roles)
            {
                UserRole? role = await dBContext.UserRoles.FindAsync(roleId);
                if (role != null)
                {
                    user.Roles.Add(role);
                }
            }
            await dBContext.SaveChangesAsync(cancellationToken);
            return mapper.Map<UserDTO>(user);
        }
    }
}
