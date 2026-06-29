using Helm.Core.Application.Common;
using Helm.Core.Application.Interfaces;
using Helm.Core.Application.UserRoles.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Helm.Core.Application.Equipment.Equipment.Queries
{
    [RequireRole("EquipmentManager")]
    public record GetAllEquipmentQuery : IRequest<GetOperationResult<List<EquipmentDTO>>>;
    public class GetAllEquipmenQueryHandler : IRequestHandler<GetAllEquipmentQuery, GetOperationResult<List<EquipmentDTO>>>
    {
        private IEquipmentRepository equipmentRepository;
        public GetAllEquipmenQueryHandler(IEquipmentRepository equipmentRepository)
        {
            this.equipmentRepository = equipmentRepository;
        }
        public async Task<GetOperationResult<List<EquipmentDTO>>> Handle(GetAllEquipmentQuery request, CancellationToken cancellationToken)
        {

            List<EquipmentDTO> vm = await equipmentRepository.GetAllEquipmentAsync(cancellationToken);
            return new GetOperationResult<List<EquipmentDTO>>.Success(vm);
        }
    }
}
