using Helm.Core.Application.Equipment.Equipment.Queries;


namespace Helm.Core.Application.Interfaces
{
    public interface IEquipmentRepository
    {
        Task<List<EquipmentDTO>> GetAllEquipmentAsync(CancellationToken cancellationToken);
        Task<EquipmentDTO> AddEquipmentAsync(Helm.Core.Domain.Entities.Equipment equipment, CancellationToken cancellationToken);
        Task<Helm.Core.Domain.Entities.Equipment?> FindEquipmentByNameAsync (string name, CancellationToken cancellationToken);
        Task<bool> IsEquipmentExistsAsync(string name, CancellationToken cancellationToken);

    }
}
