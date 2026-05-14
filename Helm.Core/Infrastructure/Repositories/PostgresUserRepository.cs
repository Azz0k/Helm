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

        public Task DeleteUserAsync(User user)
        {
            throw new NotImplementedException();
        }

        public async Task<List<UserDTO>> GetAllUsersAsync(CancellationToken cancellationToken)
        {
            return await dBContext.Users
                .AsNoTracking()
                .Where(u => !u.Deleted)
                .ProjectTo<UserDTO>(mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
        }

        public async Task<User?> FindUserByIdAysnc(int id, CancellationToken cancellationToken)
        {
            User? user = await dBContext.Users.FindAsync(id, cancellationToken);
            return user;
        }

        public Task UpdateUserAsync(User user)
        {
            throw new NotImplementedException();
        }

        public async Task<User?> FindUserByLoginAsync(string login, CancellationToken cancellationToken)
        {
            User? user = await dBContext.Users.FirstOrDefaultAsync(user => user.Login == login, cancellationToken);
            return user;
        }
    }
}
