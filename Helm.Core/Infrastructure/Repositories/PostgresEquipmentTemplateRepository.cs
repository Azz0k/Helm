using AutoMapper;
using AutoMapper.QueryableExtensions;
using Helm.Core.Application.Equipment.EquipmentTemplate.Queries;
using Helm.Core.Application.Interfaces;
using Helm.Core.Domain.Entities;
using Helm.Core.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;


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
            throw new NotImplementedException();
        }

        public async Task<EquipmentTemplate?> FindEquipmentTemplateByIdAsync(int id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<EquipmentTemplate?> FindEquipmentTemplateByNameAsync(string name, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<List<EquipmentTemplateDTO>> GetAllEquipmentTemplatesAsync(CancellationToken cancellationToken)
        {
            return await dBContext.EquipmentTemplates
                .AsNoTracking()
                .ProjectTo<EquipmentTemplateDTO>(mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
        }

        public async Task<bool> IsEquipmentTemplateExistsAsync(string name, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> IsEquipmentTemplateExistsAsync(int id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task SaveAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
