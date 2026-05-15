using FluentValidation;
using Helm.Core.Application.Common;
using Helm.Core.Application.Interfaces;
using Helm.Core.Application.Users.Queries;
using Helm.Core.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Helm.Core.Application.Users.Commands
{
    public record UpdateUserStatusCommand : IRequest<GetOperationResult<UserDTO>>
    {
        public int Id { get; set; }
        public required bool Enabled { get; set; }
    }
    public class UpdateUserStatusCommandHandler : IRequestHandler<UpdateUserStatusCommand, GetOperationResult<UserDTO>>
    {
        private IUserRepository userRepository;
        private IValidator<UpdateUserStatusCommand> validator;
        public UpdateUserStatusCommandHandler (IUserRepository userRepository, IValidator<UpdateUserStatusCommand> validator)
        {
            this.userRepository = userRepository;
            this.validator = validator;
        }
        public async Task<GetOperationResult<UserDTO>> Handle(UpdateUserStatusCommand request, CancellationToken cancellationToken)
        {
            User? user = await userRepository.FindUserByIdAysnc(request.Id, cancellationToken);
            if (user == null)
            {
                return new GetOperationResult<UserDTO>.NotFound();
            }
            user.Enabled = request.Enabled;
            UserDTO dto = await userRepository.UpdateUserAsync(user, cancellationToken);
            return new GetOperationResult<UserDTO>.Success(dto);
        }
    }
}
