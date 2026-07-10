using Helm.Application.Common;
using Helm.Application.Interfaces;
using MediatR;

namespace Helm.Application.Users.Queries
{
    [RequireRole("UserManager")]
    public record GetUsersQuery : IRequest<GetOperationResult<List<UserDTO>>>;
    public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, GetOperationResult<List<UserDTO>>>
    {
        //private PostgresUserRepository postgresUserRepository;
        private IUserRepository postgresUserRepository;
        public GetUsersQueryHandler(IUserRepository repository) 
        {
            postgresUserRepository = repository;
        }
        public async Task<GetOperationResult<List<UserDTO>>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            List<UserDTO> dto = await postgresUserRepository.GetAllUsersAsync(cancellationToken);
            return new GetOperationResult<List<UserDTO>>.Success(dto);
        }
    }

}
