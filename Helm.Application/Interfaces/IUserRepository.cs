using Helm.Application.Users.Queries;
using Helm.Domain.Entities;

namespace Helm.Application.Interfaces
{
    public interface IUserRepository
    {
        Task AddUserAsync(User user);
        Task<User?> FindUserByIdAysnc(int id, CancellationToken cancellationToken);
        Task<User?> FindUserByLoginAsync(string login,CancellationToken cancellationToken);
        Task<List<UserDTO>> GetAllUsersAsync(CancellationToken cancellationToken);
        Task SaveChangesAsync(CancellationToken cancellationToken);
    }
}
