using Helm.Core.Application.Equipment.Equipment.Queries;


namespace Helm.Core.Application.Interfaces
{
    public interface IEquipmentRepository
    {
        Task<List<EquipmentDTO>> GetAllEquipmentAsync(CancellationToken cancellationToken);
    }
}
