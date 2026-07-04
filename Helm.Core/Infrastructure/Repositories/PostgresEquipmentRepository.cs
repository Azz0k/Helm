using AutoMapper;
using AutoMapper.QueryableExtensions;
using Helm.Core.Application.Equipment.Equipment.Queries;
using Helm.Core.Application.Interfaces;
using Helm.Core.Domain.Entities;
using Helm.Core.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;


namespace Helm.Core.Infrastructure.Repositories
{
    public class PostgresEquipmentRepository : IEquipmentRepository
    {
        private PostgresDBContext dBContext;
        private IMapper mapper;
        public PostgresEquipmentRepository(PostgresDBContext dBContext, IMapper mapper)
        {
            this.dBContext = dBContext;
            this.mapper = mapper;
        }

        public async Task<EquipmentDTO> AddEquipmentAsync(Equipment equipment, CancellationToken cancellationToken)
        {
            await dBContext.Equipment.AddAsync(equipment, cancellationToken);
            await dBContext.SaveChangesAsync(cancellationToken);
            return mapper.Map<EquipmentDTO>(equipment);
        }
        public async Task SaveAsync(CancellationToken cancellationToken)
        {
            await dBContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<Equipment?> FindEquipmentByNameAsync(string name, CancellationToken cancellationToken)
        {
            return await dBContext.Equipment.Include(e => e.CreatedBy).Include(e=>e.LastModifiedBy).FirstOrDefaultAsync(e=>e.Name == name,cancellationToken);
        }
        public async Task<Equipment?> FindEquipmentByIdAsync(int id, CancellationToken cancellationToken)
        {
            return await dBContext.Equipment.Include(e => e.CreatedBy).Include(e => e.LastModifiedBy).FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
        }

        public async Task<List<EquipmentDTO>> GetAllEquipmentAsync(CancellationToken cancellationToken)
        {
            return await dBContext.Equipment
                .AsNoTracking()
                .ProjectTo<EquipmentDTO>(mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
        }

        public async Task<bool> IsEquipmentExistsAsync(string name, CancellationToken cancellationToken)
        {
            return await dBContext.Equipment.FirstOrDefaultAsync(e => e.Name == name, cancellationToken)!=null;
        }
        public async Task<bool> IsEquipmentExistsAsync(int id, CancellationToken cancellationToken)
        {
            return await dBContext.Equipment.FindAsync(id, cancellationToken) != null;
        }
    }
}
