using AutoMapper;
using Helm.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Helm.Application.UserRoles.Queries
{
    public class UserRoleDTO
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }

        public class Mapping : Profile
        {
            public Mapping()
            {
                CreateMap<UserRole, UserRoleDTO>();
            }
        }
    }
}
