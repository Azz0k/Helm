using FluentValidation;
using Helm.Application.Common;
using Helm.Application.Interfaces;
using Helm.Application.Users.Queries;
using Helm.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Helm.Application.Users.Commands
{
    [RequireRole("UserManager")]
    public record DeleteUserCommand : IRequest<GetOperationResult<UserDTO>>
    {
        public required int Id { get; set; }
    }
    public class DeleteUserHandler : IRequestHandler<DeleteUserCommand, GetOperationResult<UserDTO>>
    {
        private IUserRepository userRepository;
        public DeleteUserHandler(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task<GetOperationResult<UserDTO>> Handle(DeleteUserCommand command, CancellationToken cancellationToken)
        {
            User? user = await userRepository.FindUserByIdAysnc(command.Id, cancellationToken);
            if (user == null)
            {
                return new GetOperationResult<UserDTO>.NotFound();
            }
            user.Delete();
            await userRepository.SaveChangesAsync(cancellationToken);
            return new GetOperationResult<UserDTO>.Success(null);
        }
    }
}