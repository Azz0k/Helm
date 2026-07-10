using AutoMapper;
using Helm.Core.Application.Common;
using Helm.Core.Application.Interfaces;
using Helm.Core.Application.Users.Queries;
using Helm.Domain.Entities;
using MediatR;

namespace Helm.Core.Application.Users.Commands
{
    [RequireRole("UserManager")]
    public record ReplaceUserRoleCommand : IRequest<GetOperationResult<UserDTO>>
    {
        public int UserId { get; set; }
        public List<int> Roles { get; set; } = [];
    }
    public class ReplaceUserRoleHandler : IRequestHandler<ReplaceUserRoleCommand, GetOperationResult<UserDTO>>
    {
        private IUserRepository userRepository;
        private IUserRoleRepository userRoleRepository;
        private IMapper mapper;
        public ReplaceUserRoleHandler(IUserRepository userRepository, IUserRoleRepository userRoleRepository, IMapper mapper)
        {
            this.userRepository = userRepository;
            this.userRoleRepository = userRoleRepository;
            this.mapper = mapper;
        }
        public async Task<GetOperationResult<UserDTO>> Handle(ReplaceUserRoleCommand request, CancellationToken cancellationToken)
        {
            User? user = await userRepository.FindUserByIdAysnc(request.UserId, cancellationToken);
            if (user == null)
            {
                return new GetOperationResult<UserDTO>.NotFound();
            }
            user.ClearRoles();
            foreach (var roleId in request.Roles)
            {
                UserRole? userRole = await userRoleRepository.FindByIdAsync(roleId);
                if (userRole != null)
                {
                    user.AddRole(userRole);
                }
            }
            await userRepository.SaveChangesAsync(cancellationToken);
            UserDTO dto = mapper.Map<UserDTO>(user);
            return new GetOperationResult<UserDTO>.Success(dto);
        }
    }


}
