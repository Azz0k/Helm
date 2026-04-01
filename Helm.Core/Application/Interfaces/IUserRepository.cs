using Helm.Core.Application.Users.Queries;
using Helm.Core.Domain.ApiModels.User;
using Helm.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Helm.Core.Application.Interfaces
{
    public interface IUserRepository
    {
        Task AddUserAsync(AddUserRequest request);
        Task<User> GetUserAysnc(int id);
        Task<UsersVm> GetAllUsersAsync(CancellationToken cancellationToken);
        Task UpdateUserAsync(User user);
        Task DeleteUserAsync(User user);
    }
}
