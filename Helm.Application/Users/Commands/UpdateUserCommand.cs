using AutoMapper;
using FluentValidation;
using FluentValidation.Validators;
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
    public record UpdateUserCommand : IRequest<GetOperationResult<UserDTO>>
    {
        public int Id { get; set; }
        public required string Login { get; set; }
        public required string Name { get; set; }
    }
    public class UpdateUserHandler : IRequestHandler<UpdateUserCommand, GetOperationResult<UserDTO>>
    {
        private IUserRepository userRepository;
        private IMapper mapper;
        public UpdateUserHandler(IUserRepository userRepository, IMapper mapper) 
        { 
            this.userRepository = userRepository;
            this.mapper = mapper;
        }
        public async Task<GetOperationResult<UserDTO>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            User? user = await userRepository.FindUserByIdAysnc(request.Id, cancellationToken);
            if (user == null)
            {
                return new GetOperationResult<UserDTO>.NotFound();
            }
            string newLogin = request.Login.ToLower();
            if (newLogin != user.Login)
            {
                User? targetUser = await userRepository.FindUserByLoginAsync(newLogin, cancellationToken);
                if (targetUser == null)
                {
                    user.ChangeLogin(newLogin);
                }
                else
                {
                    return new GetOperationResult<UserDTO>.Conflict();
                }
            }
            user.Rename(request.Name);
            await userRepository.SaveChangesAsync(cancellationToken);
            UserDTO dto = mapper.Map<UserDTO>(user);
            return new GetOperationResult<UserDTO>.Success(dto);
        }
    }

}
