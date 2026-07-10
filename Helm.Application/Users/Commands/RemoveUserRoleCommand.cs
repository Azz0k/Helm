
using AutoMapper;
using Helm.Application.Common;
using Helm.Application.Interfaces;
using Helm.Application.Users.Queries;
using Helm.Domain.Entities;
using MediatR;

namespace Helm.Application.Users.Commands
{
    [RequireRole("UserManager")]
    public record RemoveUserRoleCommand : IRequest<GetOperationResult<UserDTO>>
    {
        public int UserId { get; set; }
        public int RoleId { get; set; }
    }
    public class RemoveUserRoleHandler : IRequestHandler<RemoveUserRoleCommand, GetOperationResult<UserDTO>>
    {
        private IUserRepository userRepository;
        private IUserRoleRepository roleRepository;
        private IMapper mapper;
        public RemoveUserRoleHandler(IUserRepository userRepository, IUserRoleRepository roleRepository, IMapper mapper)
        {
            this.userRepository = userRepository;
            this.roleRepository = roleRepository;
            this.mapper = mapper;
        }

        public async Task<GetOperationResult<UserDTO>> Handle(RemoveUserRoleCommand request, CancellationToken cancellationToken)
        {
            User? user = await userRepository.FindUserByIdAysnc(request.UserId, cancellationToken);
            if (user == null)
            {
                return new GetOperationResult<UserDTO>.NotFound();
            }
            if (!user.HasRole(request.RoleId))
            {
                return new GetOperationResult<UserDTO>.Invalid();
            }
            UserRole? userRole = await roleRepository.FindByIdAsync(request.RoleId);
            if (userRole == null)
            {
                return new GetOperationResult<UserDTO>.Invalid();
            }
            user.RemoveRole(userRole);
            await userRepository.SaveChangesAsync(cancellationToken);
            UserDTO dto = mapper.Map<UserDTO>(user);
            return new GetOperationResult<UserDTO>.Success(dto);
        }
    }
}
