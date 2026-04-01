using AutoMapper;
using Helm.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;


namespace Helm.Core.Application.Users.Queries
{
    public class UsersVm
    {
        public IReadOnlyCollection<UserDTO> Users { get; init; } = [];
    }
    public class UserDTO
    {
        public int Id { get; set; }
        public required string Login { get; set; }
        public required string Name { get; set; }
        public string? ADLogin { get; set; }
        public bool Enabled { get; set; }
        public required List<string> Roles { get; set; }
        public class Mapping : Profile
        {
            public Mapping()
            {
                CreateMap<User, UserDTO>()
                    .ForMember(dest => dest.Roles, opt => opt.MapFrom(src => src.Roles.Select(x => x.Name)));
            }
        }
    }
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
