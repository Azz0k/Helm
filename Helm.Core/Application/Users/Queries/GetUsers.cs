using Helm.Core.Infrastructure.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Helm.Core.Application.Users.Queries
{
    public record GetUsersQuery : IRequest<UsersVm>;
    public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, UsersVm>
    {
        private PostgresUserRepository postgresUserRepository;
        public GetUsersQueryHandler(PostgresUserRepository repository) 
        {
            postgresUserRepository = repository;
        }
        public async Task<UsersVm> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            return await postgresUserRepository.GetAllUsersAsync(cancellationToken);
        }
    }

}
