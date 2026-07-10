using AutoMapper;
using Helm.Application.Common;
using Helm.Application.Interfaces;
using Helm.Application.UserRoles.Queries;
using Helm.Domain.Entities;
using MediatR;

namespace Helm.Application.UserRoles.Commands
{
    [RequireRole("UserRoleManager")]
    public record CreateUserRoleCommand: IRequest<GetOperationResult<UserRoleDTO>>
    {
        public required string Name { get; set; }
        public string? Description { get; set; }
    }
    public class CreateUserRoleHandler : IRequestHandler<CreateUserRoleCommand, GetOperationResult<UserRoleDTO>>
    {
        private IUserRoleRepository userRoleRepository;
        private IMapper mapper;
        public CreateUserRoleHandler(IUserRoleRepository userRoleRepository, IMapper mapper)
        {
            this.userRoleRepository = userRoleRepository;
            this.mapper = mapper;
        }
        public async Task<GetOperationResult<UserRoleDTO>> Handle(CreateUserRoleCommand command, CancellationToken cancellationToken)
        {
            UserRole? role = await userRoleRepository.FindByNameAsync(command.Name, cancellationToken);
            if (role != null)
            {
                return new GetOperationResult<UserRoleDTO>.Conflict();
            }
            var entity = new UserRole(command.Name, command.Description);
            await userRoleRepository.AddRoleAsync(entity);
            await userRoleRepository.SaveChangesAsync(cancellationToken);
            UserRoleDTO dto = mapper.Map<UserRoleDTO>(entity);
            return new GetOperationResult<UserRoleDTO>.Success(dto);
        }
    }
}
