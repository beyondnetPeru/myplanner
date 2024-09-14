using Microsoft.EntityFrameworkCore;
using MyPlanner.Plannings.Api.Dtos.SizeModelType;
using MyPlanner.Plannings.Infrastructure.Database;
using MyPlanner.Plannings.Shared.Application.Dtos;

namespace MyPlanner.Plannings.Api.UseCases.SizeModelTypes.Queries
{
    public class SizeModelTypeQueryRepository : ISizeModelTypeQueryRepository
    {
        private readonly PlanningDbContext context;
        private readonly IMapper mapper;

        public SizeModelTypeQueryRepository(PlanningDbContext context, IMapper mapper)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<IEnumerable<SizeModelTypeDto>> GetAll(PaginationDto pagination)
        {
            var data = await context.SizeModelTypes
            .OrderBy(c => c.Name)
            .Skip(pagination.RecordsPerPage * pagination.Page)
            .Take(pagination.RecordsPerPage)
            .ToListAsync();

            var entities = mapper.Map<ICollection<SizeModelTypeDto>>(data);

            return entities;
        }


        public async Task<SizeModelTypeDto> GetById(string id)
        {

            var data = await context.SizeModelTypes.FirstOrDefaultAsync(x => x.Id == id);

            var entity = mapper.Map<SizeModelTypeDto>(data);

            return entity;
        }

        public async Task<SizeModelTypeDto> GetByCode(string code)
        {
            var data = await context.SizeModelTypes.FirstOrDefaultAsync(x => x.Code == code);

            var entity = mapper.Map<SizeModelTypeDto>(data);

            return entity;
        }

        public async Task<IEnumerable<SizeModelTypeFactorDto>> GetFactors(string sizeModelTypeId)
        {
            var data = await context.SizeModelTypeFactors.Where(x => x.SizeModelTypeId == sizeModelTypeId).ToListAsync();

            var dtos = mapper.Map<ICollection<SizeModelTypeFactorDto>>(data);

            return dtos;
        }

        public async Task<SizeModelTypeFactorDto> GetFactorById(string id)
        {
            var data = await context.SizeModelTypeFactors.FirstOrDefaultAsync(x => x.Id == id);

            var dto = mapper.Map<SizeModelTypeFactorDto>(data);

            return dto;
        }

        public async Task<SizeModelTypeFactorDto> GetFactorByCode(string code)
        {
            var data = await context.SizeModelTypeFactors.FirstOrDefaultAsync(x => x.Code == code);

            var dto = mapper.Map<SizeModelTypeFactorDto>(data);

            return dto;
        }
    }
}
