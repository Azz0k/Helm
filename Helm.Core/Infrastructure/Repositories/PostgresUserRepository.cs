using AutoMapper;
using AutoMapper.QueryableExtensions;
using Helm.Core.Application.Interfaces;
using Helm.Core.Application.Users.Queries;
using Helm.Core.Domain.ApiModels.User;
using Helm.Core.Domain.Entities;
using Helm.Core.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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
        public Task AddUserAsync(AddUserRequest request)
        {
            throw new NotImplementedException();
        }

        public Task DeleteUserAsync(User user)
        {
            throw new NotImplementedException();
        }

        public async Task<UsersVm> GetAllUsersAsync(CancellationToken cancellationToken)
        {
            return new UsersVm()
            {
                Users = await dBContext.Users
                .AsNoTracking()
                .Where(u => !u.Deleted)
                .ProjectTo<UserDTO>(mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken)
            };
        }

        public Task<User> GetUserAysnc(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateUserAsync(User user)
        {
            throw new NotImplementedException();
        }
    }
}
