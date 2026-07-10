using AutoMapper;
using Helm.Application.Common;
using Helm.Application.Interfaces;
using Helm.Application.Users.Queries;
using Helm.Domain.Entities;
using MediatR;


namespace Helm.Application.Users.Commands
{
    [RequireRole("UserManager")]
    public record UpdateUserStatusCommand : IRequest<GetOperationResult<UserDTO>>
    {
        public int Id { get; set; }
        public required bool Enabled { get; set; }
    }
    public class UpdateUserStatusCommandHandler : IRequestHandler<UpdateUserStatusCommand, GetOperationResult<UserDTO>>
    {
        private IUserRepository userRepository;
        private IMapper mapper;
        public UpdateUserStatusCommandHandler (IUserRepository userRepository, IMapper mapper)
        {
            this.userRepository = userRepository;
            this.mapper = mapper;
        }
        public async Task<GetOperationResult<UserDTO>> Handle(UpdateUserStatusCommand request, CancellationToken cancellationToken)
        {
            User? user = await userRepository.FindUserByIdAysnc(request.Id, cancellationToken);
            if (user == null)
            {
                return new GetOperationResult<UserDTO>.NotFound();
            }
            user.SetStatus(request.Enabled);
            await userRepository.SaveChangesAsync(cancellationToken);
            UserDTO dto = mapper.Map<UserDTO>(user);
            return new GetOperationResult<UserDTO>.Success(dto);
        }
    }
}
