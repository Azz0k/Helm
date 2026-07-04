using AutoMapper;
using Helm.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Helm.Core.Application.Equipment.EquipmentTemplate.Queries
{
    public class EquipmentTemplateDTO
    {
        public int Id { get; init; }
        public string Name { get; init; }
        public string Description { get; init; } 
        public bool Enabled { get; init; }
        public DateTimeOffset CreatedAt { get; init; }
        public string CreatedBy { get; init; }
        public DateTimeOffset? LastModifiedAt { get; init; }
        public string? LastModifiedBy { get; init; }
        public class Mapping : Profile
        {
            public Mapping()
            {
                CreateMap<Helm.Core.Domain.Entities.EquipmentTemplate, EquipmentTemplateDTO>()
                    .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedBy.Name))
                    .ForMember(dest => dest.LastModifiedBy, opt => opt.MapFrom(src => src.LastModifiedBy.Name));
            }
        }
    }
}
