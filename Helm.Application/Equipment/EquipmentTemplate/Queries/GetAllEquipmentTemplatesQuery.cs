using Helm.Application.Common;
using Helm.Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Helm.Application.Equipment.EquipmentTemplate.Queries
{
    [RequireRole("EquipmentManager")]
    public record GetAllEquipmentTemplatesQuery : IRequest<GetOperationResult<List<EquipmentTemplateDTO>>>;
    public class GetAllEquipmentTemplatesQueryHandler : IRequestHandler<GetAllEquipmentTemplatesQuery, GetOperationResult<List<EquipmentTemplateDTO>>>
    {
        private IEquipmentTemplateRepository equipmentTemplateRepository;
        public GetAllEquipmentTemplatesQueryHandler(IEquipmentTemplateRepository equipmentTemplateRepository)
        {
            this.equipmentTemplateRepository = equipmentTemplateRepository;
        }
        public async Task<GetOperationResult<List<EquipmentTemplateDTO>>> Handle(GetAllEquipmentTemplatesQuery request, CancellationToken cancellationToken)
        {
            List<EquipmentTemplateDTO> equipmentTemplateDTOs = await equipmentTemplateRepository.GetAllEquipmentTemplatesAsync(cancellationToken);
            return new GetOperationResult<List<EquipmentTemplateDTO>>.Success(equipmentTemplateDTOs);
        }
    }
}
