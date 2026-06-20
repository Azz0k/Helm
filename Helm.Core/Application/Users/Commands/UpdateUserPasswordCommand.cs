using FluentValidation;
using Helm.Core.Application.Common;
using Helm.Core.Application.Interfaces;
using Helm.Core.Application.Users.Queries;
using Helm.Core.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Helm.Core.Application.Users.Commands
{
    [RequireRole("UserManager")]
    public record UpdateUserPasswordCommand : IRequest<GetOperationResult<UserDTO>>
    {
        public int Id { get; set; }
        public required string Password { get; set; }
    }
    public class UpdateUserPasswordHandler : IRequestHandler<UpdateUserPasswordCommand, GetOperationResult<UserDTO>>
    {
        private IUserRepository userRepository;
        private IValidator<UpdateUserPasswordCommand> validator;
        public UpdateUserPasswordHandler(IUserRepository userRepository, IValidator<UpdateUserPasswordCommand> validator)
        {
            this.userRepository = userRepository;
            this.validator = validator;
        }
        public async Task<GetOperationResult<UserDTO>> Handle(UpdateUserPasswordCommand request, CancellationToken cancellationToken)
        {
            User? user = await userRepository.FindUserByIdAysnc(request.Id, cancellationToken);
            if (user == null)
            {
                return new GetOperationResult<UserDTO>.NotFound();
            }
            user.Hash = BCrypt.Net.BCrypt.HashPassword(request.Password);
            UserDTO dto = await userRepository.UpdateUserAsync(user, cancellationToken);
            return new GetOperationResult<UserDTO>.Success(dto);
        }
    }
}
