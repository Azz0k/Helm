using Helm.Application.Equipment.Equipment.Queries;

namespace Helm.Application.Interfaces
{
    public interface IEquipmentRepository
    {
        Task<List<EquipmentDTO>> GetAllEquipmentAsync(CancellationToken cancellationToken);
        Task<EquipmentDTO> AddEquipmentAsync(Domain.Entities.Equipment equipment, CancellationToken cancellationToken);
        Task<Domain.Entities.Equipment?> FindEquipmentByNameAsync (string name, CancellationToken cancellationToken);
        Task<Domain.Entities.Equipment?> FindEquipmentByIdAsync(int id, CancellationToken cancellationToken);
        Task<bool> IsEquipmentExistsAsync(string name, CancellationToken cancellationToken);
        Task<bool> IsEquipmentExistsAsync(int id, CancellationToken cancellationToken);
        Task SaveAsync(CancellationToken cancellationToken);

    }
}
