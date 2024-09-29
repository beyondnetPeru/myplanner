using Microsoft.EntityFrameworkCore;
using MyPlanner.Plannings.Api.Dtos.SizeModelType;
using MyPlanner.Plannings.Infrastructure.Database;
using MyPlanner.Shared.Models.Pagination.Dtos;

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
            .Include(x => x.Items)
            .OrderBy(c => c.Name)
            .Skip(pagination.RecordsPerPage * pagination.Page)
            .Take(pagination.RecordsPerPage)
            .ToListAsync();

            var entities = mapper.Map<ICollection<SizeModelTypeDto>>(data);

            return entities;
        }


        public async Task<SizeModelTypeDto> GetById(string id)
        {

            var data = await context.SizeModelTypes.Include(x => x.Items).FirstOrDefaultAsync(x => x.Id == id);

            var entity = mapper.Map<SizeModelTypeDto>(data);

            return entity;
        }

        public async Task<SizeModelTypeDto> GetByCode(string code)
        {
            var data = await context.SizeModelTypes.Include(x => x.Items).FirstOrDefaultAsync(x => x.Code == code);

            var entity = mapper.Map<SizeModelTypeDto>(data);

            return entity;
        }

        public async Task<IEnumerable<SizeModelTypeItemDto>> GetItems(string sizeModelTypeId)
        {
            var data = await context.SizeModelTypeItems.Where(x => x.SizeModelType.Id == sizeModelTypeId).Include(x => x.SizeModelType).ToListAsync();

            var dtos = mapper.Map<ICollection<SizeModelTypeItemDto>>(data);

            return dtos;
        }

        public async Task<SizeModelTypeItemDto> GetItemById(string id)
        {
            var data = await context.SizeModelTypeItems.FirstOrDefaultAsync(x => x.Id == id);

            var dto = mapper.Map<SizeModelTypeItemDto>(data);

            return dto;
        }

        public async Task<SizeModelTypeItemDto> GetItemByCode(string code)
        {
            var data = await context.SizeModelTypeItems.FirstOrDefaultAsync(x => x.Code == code);

            var dto = mapper.Map<SizeModelTypeItemDto>(data);

            return dto;
        }
    }
}
