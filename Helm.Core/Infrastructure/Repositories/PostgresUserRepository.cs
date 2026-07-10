using AutoMapper;
using AutoMapper.QueryableExtensions;
using Helm.Application.Interfaces;
using Helm.Application.UserRoles.Queries;
using Helm.Application.Users.Queries;
using Helm.Core.Infrastructure.Contexts;
using Helm.Domain.Entities;
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
        public async Task AddUserAsync(User user)
        {
            await dBContext.Users.AddAsync(user);
        }
        public async Task<List<UserDTO>> GetAllUsersAsync(CancellationToken cancellationToken)
        {
            return await dBContext.Users
                .Where(u=>!u.Deleted)
                .AsNoTracking()
                .ProjectTo<UserDTO>(mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
        }
        public async Task<User?> FindUserByIdAysnc(int id, CancellationToken cancellationToken)
        {
            User? user = await dBContext.Users.Include(p => p.Roles).FirstOrDefaultAsync(u => u.Id == id, cancellationToken);
            return user;
        }
        public async Task<User?> FindUserByLoginAsync(string login, CancellationToken cancellationToken)
        {
            User? user = await dBContext.Users.Include(p => p.Roles).FirstOrDefaultAsync(user => user.Login == login, cancellationToken);
            return user;
        }
        public async Task SaveChangesAsync(CancellationToken cancellationToken)
        {
            await dBContext.SaveChangesAsync(cancellationToken);
        }
    }
}
