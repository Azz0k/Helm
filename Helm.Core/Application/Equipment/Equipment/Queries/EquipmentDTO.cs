using AutoMapper;


namespace Helm.Core.Application.Equipment.Equipment.Queries
{
    public class EquipmentDTO
    {
        public int Id { get; init; }
        public required string Name { get; init; }
        public bool IsIssued { get; init; } 
        public required string IssuedBy { get; init; } 
        public bool IsLost { get; init; } 
        public bool IsBulk { get; init; } 
        public string CreatedBy { get; init; }
        public required DateTimeOffset CreatedAt { get; init; } 
        public DateTimeOffset? LastModifiedAt { get; init; }
        public string? LastModifiedBy { get; init; }
        public class Mapping : Profile
        {
            public Mapping()
            {
                CreateMap<Domain.Entities.Equipment, EquipmentDTO>()
                    .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedBy.Name))
                    .ForMember(dest => dest.LastModifiedBy, opt => opt.MapFrom(src => src.LastModifiedBy.Name));
            }
        }
    }
}
