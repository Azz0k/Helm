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
    public record DeleteUserCommand : IRequest<GetOperationResult<object>>
    {
        public required int Id { get; set; }
    }
    public class DeleteUserHandler : IRequestHandler<DeleteUserCommand, GetOperationResult<object>>
    {
        private IUserRepository userRepository;
        private readonly IValidator<CreateUserCommand> validator;
        public DeleteUserHandler(IUserRepository userRepository,  IValidator<CreateUserCommand> validator)
        {
            this.userRepository = userRepository;
            this.validator = validator;
        }

        public async Task<GetOperationResult<object>> Handle(DeleteUserCommand command, CancellationToken cancellationToken)
        {
            if (await userRepository.DeleteUserAsync(command.Id, cancellationToken))
            {
                return new GetOperationResult<object>.Success(new Object());
            }
            return new GetOperationResult<object>.NotFound();
        }
    }
}