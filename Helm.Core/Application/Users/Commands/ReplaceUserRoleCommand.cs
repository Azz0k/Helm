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
    public record ReplaceUserRoleCommand : IRequest<GetOperationResult<UserDTO>>
    {
        public int UserId { get; set; }
        public List<int> Roles { get; set; } = [];
    }
    public class ReplaceUserRoleHandler : IRequestHandler<ReplaceUserRoleCommand, GetOperationResult<UserDTO>>
    {
        private IUserRepository userRepository;
        private IValidator<ReplaceUserRoleCommand> validator;
        public ReplaceUserRoleHandler(IUserRepository userRepository, IValidator<ReplaceUserRoleCommand> validator)
        {
            this.userRepository = userRepository;
            this.validator = validator;
        }

        public async Task<GetOperationResult<UserDTO>> Handle(ReplaceUserRoleCommand request, CancellationToken cancellationToken)
        {
            User? user = await userRepository.FindUserByIdAysnc(request.UserId, cancellationToken);
            if (user == null)
            {
                return new GetOperationResult<UserDTO>.NotFound();
            }
            var dto = await userRepository.ReplaceUserRoleAsync(user, request.Roles, cancellationToken);
            return new GetOperationResult<UserDTO>.Success(dto);
        }
    }


}
