using Helm.Core.Application.Common;
using Helm.Core.Application.Interfaces;
using Helm.Core.Infrastructure.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Helm.Core.Application.Users.Queries
{
    public record GetUsersQuery : IRequest<GetOperationResult<List<UserDTO>>>;
    public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, GetOperationResult<List<UserDTO>>>
    {
        //private PostgresUserRepository postgresUserRepository;
        private IUserRepository postgresUserRepository;
        public GetUsersQueryHandler(IUserRepository repository) 
        {
            postgresUserRepository = repository;
        }
        public async Task<GetOperationResult<List<UserDTO>>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            List<UserDTO> dto = await postgresUserRepository.GetAllUsersAsync(cancellationToken);
            return new GetOperationResult<List<UserDTO>>.Success(dto);
        }
    }

}
