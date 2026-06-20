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
    [RequireRole("UserManager")]
    public record UpdateUserCommand : IRequest<GetOperationResult<UserDTO>>
    {
        public int Id { get; set; }
        public string? Login { get; set; }
        public  string? Name { get; set; }
        public string? ADLogin { get; set; }
    }
    public class UpdateUserHandler : IRequestHandler<UpdateUserCommand, GetOperationResult<UserDTO>>
    {
        private IUserRepository userRepository;
        private IValidator<UpdateUserCommand> validator;
        public UpdateUserHandler(IUserRepository userRepository, IValidator<UpdateUserCommand> validator) 
        { 
            this.userRepository = userRepository;
            this.validator = validator;
        }
        public async Task<GetOperationResult<UserDTO>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            User? user = await userRepository.FindUserByIdAysnc(request.Id, cancellationToken);
            if (user == null)
            {
                return new GetOperationResult<UserDTO>.NotFound();
            }

            if (request.Login != null)
            {
                user.Login = request.Login;
            }
            if (request.Name != null)
            {
                user.Name = request.Name;
            }
            if (request.ADLogin != null)
            {
                user.ADLogin = request.ADLogin;
            }
            var dto = await userRepository.UpdateUserAsync(user, cancellationToken);
            return new GetOperationResult<UserDTO>.Success(dto);
        }
    }

}
