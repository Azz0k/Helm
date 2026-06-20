using AutoMapper;
using Helm.Core.Application.Common;
using Helm.Core.Application.Interfaces;
using Helm.Core.Application.UserRoles.Queries;
using Helm.Core.Domain.Entities;
using Helm.Core.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Helm.Core.Infrastructure.Repositories
{
    public class PostgresAuthorizationService : IAuthorizationService
    {
        private PostgresDBContext dBContext;
        private IMapper mapper;
        public PostgresAuthorizationService(PostgresDBContext dBContext, IMapper mapper)
        {
            this.dBContext = dBContext;
            this.mapper = mapper;
        }

        public async Task<CurrentUserRolesDTO> GetCurrentUserRolesAsync(string? login, CancellationToken cancellationToken)
        {
            if (login == null)
            {
                return new CurrentUserRolesDTO();
            }
            User? user = await dBContext.Users.Include(p => p.Roles).FirstOrDefaultAsync(user => user.Login == login.ToLower(), cancellationToken);
            if (user == null)
            {
                return new CurrentUserRolesDTO(); 
            }
            return mapper.Map<CurrentUserRolesDTO>(user);
        }

        public async Task<bool> IsAllowedAsync(string? login, Type? type, CancellationToken cancellationToken)
        {
            if (type == null)
            {
                return true;
            }
            var customAttributes = (RequireRole[])type.GetCustomAttributes(typeof(RequireRole), true);
            if (customAttributes.Length == 0)
            {
                return true;
            }
            string requiredRole = customAttributes[0].Role;
            if (string.IsNullOrEmpty(login))
            {
                return false;
            }
            login = login.ToLower();
            User? user = await dBContext.Users.Include(p => p.Roles).FirstOrDefaultAsync(user => user.Login == login, cancellationToken);
            if (user == null)
            {
                return false;
            }
            if (!user.Enabled)
            {
                return false;
            }
            foreach (var role in user.Roles)
            {
                if (role.Name == requiredRole)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
