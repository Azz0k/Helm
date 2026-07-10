using AutoMapper;
using Helm.Domain.Entities;



namespace Helm.Application.Users.Queries
{

    public class UserDTO
    {
        public int Id { get; set; }
        public required string Login { get; set; }
        public required string Name { get; set; }
        public bool Enabled { get; set; }
        public required List<int> Roles { get; set; }
        public class Mapping : Profile
        {
            public Mapping()
            {
                CreateMap<User, UserDTO>()
                    .ForMember(dest => dest.Roles, opt => opt.MapFrom(src => src.Roles.Select(x => x.Id)));
            }
        }
    }

}
