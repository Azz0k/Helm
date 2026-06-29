using AutoMapper;
using AutoMapper.QueryableExtensions;
using Helm.Core.Application.Equipment.Equipment.Queries;
using Helm.Core.Application.Interfaces;
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

        public async Task<List<EquipmentDTO>> GetAllEquipmentAsync(CancellationToken cancellationToken)
        {
            return await dBContext.Equipment
                .AsNoTracking()
                .ProjectTo<EquipmentDTO>(mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
        }
    }
}
