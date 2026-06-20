using AutoMapper;
using Helm.Core.Application.Users.Queries;
using Helm.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Helm.Core.Application.UserRoles.Queries
{
    public class CurrentUserRolesDTO
    {
        public List<string> Roles { get; set; } = [];
        public class Mapping : Profile
        {
            public Mapping()
            {
                CreateMap<User, CurrentUserRolesDTO>()
                    .ForMember(dest => dest.Roles, opt => opt.MapFrom(src => src.Roles.Select(x => x.Name)));
            }
        }
    }
}
