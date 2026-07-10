using Helm.Application.Equipment.EquipmentTemplate.Queries;
using Helm.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Helm.Application.Interfaces
{
    public interface IEquipmentTemplateRepository
    {
        Task<List<EquipmentTemplateDTO>> GetAllEquipmentTemplatesAsync(CancellationToken cancellationToken);
        Task<EquipmentTemplateDTO> AddEquipmentTemplateAsync(EquipmentTemplate equipmentTemplate,  CancellationToken cancellationToken);
        Task<EquipmentTemplate?> FindEquipmentTemplateByNameAsync(string name, CancellationToken cancellationToken);
        Task<EquipmentTemplate?> FindEquipmentTemplateByIdAsync(int id, CancellationToken cancellationToken);
        Task<bool> IsEquipmentTemplateExistsAsync(string name, CancellationToken cancellationToken);
        Task<bool> IsEquipmentTemplateExistsAsync(int id, CancellationToken cancellationToken);
        Task SaveAsync(CancellationToken cancellationToken);
    }
}
