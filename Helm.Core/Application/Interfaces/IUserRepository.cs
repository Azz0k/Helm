using Helm.Core.Application.Users.Queries;
using Helm.Core.Domain.Entities;

namespace Helm.Core.Application.Interfaces
{
    public interface IUserRepository
    {
        Task<UserDTO> AddUserAsync(User user, CancellationToken cancellationToken);
        Task<User?> FindUserByIdAysnc(int id, CancellationToken cancellationToken);
        Task<User?> FindUserByLoginAsync(string login,CancellationToken cancellationToken);
        Task<List<UserDTO>> GetAllUsersAsync(CancellationToken cancellationToken);
        Task UpdateUserAsync(User user);
        Task DeleteUserAsync(User user);
    }
}
