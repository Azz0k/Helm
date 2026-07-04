using Helm.Core.Application.Equipment.EquipmentTemplate.Queries;
using System;
using System.Collections.Generic;
using System.Text;

namespace Helm.Core.Application.Interfaces
{
    public interface IEquipmentTemplateRepository
    {
        Task<List<EquipmentTemplateDTO>> GetAllEquipmentTemplatesAsync(CancellationToken cancellationToken);
        Task<EquipmentTemplateDTO> AddEquipmentTemplateAsync(Helm.Core.Domain.Entities.EquipmentTemplate equipmentTemplate,  CancellationToken cancellationToken);
        Task<Helm.Core.Domain.Entities.EquipmentTemplate?> FindEquipmentTemplateByNameAsync(string name, CancellationToken cancellationToken);
        Task<Helm.Core.Domain.Entities.EquipmentTemplate?> FindEquipmentTemplateByIdAsync(int id, CancellationToken cancellationToken);
        Task<bool> IsEquipmentTemplateExistsAsync(string name, CancellationToken cancellationToken);
        Task<bool> IsEquipmentTemplateExistsAsync(int id, CancellationToken cancellationToken);
        Task SaveAsync(CancellationToken cancellationToken);
    }
}
