using AutoMapper;
using AutoMapper.QueryableExtensions;
using Helm.Core.Application.Equipment.EquipmentTemplate.Queries;
using Helm.Core.Application.Interfaces;
using Helm.Core.Domain.Entities;
using Helm.Core.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;


namespace Helm.Core.Infrastructure.Repositories
{
    public class PostgresEquipmentTemplateRepository : IEquipmentTemplateRepository
    {
        private PostgresDBContext dBContext;
        private IMapper mapper;
        public PostgresEquipmentTemplateRepository(PostgresDBContext dBContext, IMapper mapper)
        {
            this.dBContext = dBContext;
            this.mapper = mapper;
        }

        public async Task<EquipmentTemplateDTO> AddEquipmentTemplateAsync(EquipmentTemplate equipmentTemplate, CancellationToken cancellationToken)
        {
            await dBContext.EquipmentTemplates.AddAsync(equipmentTemplate, cancellationToken);
            await dBContext.SaveChangesAsync();
            return mapper.Map<EquipmentTemplateDTO>(equipmentTemplate);
        }

        public async Task<EquipmentTemplate?> FindEquipmentTemplateByIdAsync(int id, CancellationToken cancellationToken)
        {
            return await dBContext.EquipmentTemplates.Include(e => e.CreatedBy).Include(e => e.DeletedBy).FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
        }

        public async Task<EquipmentTemplate?> FindEquipmentTemplateByNameAsync(string name, CancellationToken cancellationToken)
        {
            return await dBContext.EquipmentTemplates.Include(e => e.CreatedBy).Include(e => e.DeletedBy).FirstOrDefaultAsync(e => e.Name == name, cancellationToken);
        }

        public async Task<List<EquipmentTemplateDTO>> GetAllEquipmentTemplatesAsync(CancellationToken cancellationToken)
        {
            return await dBContext.EquipmentTemplates
                .Where(e=>!e.Deleted)
                .AsNoTracking()
                .ProjectTo<EquipmentTemplateDTO>(mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
        }

        public async Task<bool> IsEquipmentTemplateExistsAsync(string name, CancellationToken cancellationToken)
        {
            return await dBContext.EquipmentTemplates.FirstOrDefaultAsync(e => e.Name == name, cancellationToken) != null;
        }

        public async Task<bool> IsEquipmentTemplateExistsAsync(int id, CancellationToken cancellationToken)
        {
            return await dBContext.EquipmentTemplates.FirstOrDefaultAsync(e => e.Id == id, cancellationToken) != null;
        }

        public async Task SaveAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
