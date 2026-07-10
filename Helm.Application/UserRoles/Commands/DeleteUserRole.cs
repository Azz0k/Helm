using MediatR;
using Helm.Application.UserRoles.Queries;
using Helm.Application.Common;
using Helm.Application.Interfaces;

namespace Helm.Application.UserRoles.Commands
{
    [RequireRole("UserRoleManager")]
    public record DeleteUserRoleCommand : IRequest<GetOperationResult<UserRoleDTO>>
    {
        public required int Id { get; set; }
    }
    public class DeleteUserRole : IRequestHandler<DeleteUserRoleCommand, GetOperationResult<UserRoleDTO>>
    {
        private IUserRoleRepository userRoleRepository;
        public DeleteUserRole(IUserRoleRepository userRoleRepository)
        {
            this.userRoleRepository = userRoleRepository;
        }
        public async Task<GetOperationResult<UserRoleDTO>> Handle(DeleteUserRoleCommand command, CancellationToken cancellationToken)
        {
            if (await userRoleRepository.DeleteByIdAsync(command.Id, cancellationToken))
            {
                return new GetOperationResult<UserRoleDTO>.Success(null);
            }
            return new GetOperationResult<UserRoleDTO>.NotFound();
        }
    }
}
