using Microsoft.EntityFrameworkCore;
using MyPlanner.Plannings.Api.Dtos.SizeModel;
using MyPlanner.Plannings.Infrastructure.Database;
using MyPlanner.Shared.Models.Pagination;

namespace MyPlanner.Plannings.Api.UseCases.SizeModels.Queries
{
    public class SizeModelQueryRepository : ISizeModelQueryRepository
    {
        private readonly PlanningDbContext context;
        private readonly IMapper mapper;

        public SizeModelQueryRepository(PlanningDbContext context, IMapper mapper)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<SizeModelDto> Get(string sizeModelId)
        {
            var data = await context.SizeModels.Include(x => x.Items).ThenInclude(x => x.SizeModelTypeItem).Where(x => x.Id == sizeModelId).FirstOrDefaultAsync();

            var dto = mapper.Map<SizeModelDto>(data);

            return dto;
        }

        public async Task<IEnumerable<SizeModelDto>> GetAll(PaginationQuery pagination)
        {
            var data = await context.SizeModels
                .Include(x => x.Items)
                .ThenInclude(x => x.SizeModelTypeItem)
                .Skip(pagination.Page)
                .Take(pagination.RecordsPerPage)
                .ToListAsync();

            var dto = mapper.Map<IEnumerable<SizeModelDto>>(data);

            return dto;
        }

        public async Task<SizeModelItemDto> GetItem(string sizeModelItemId)
        {
            var data = await context.SizeModelItems.FindAsync(sizeModelItemId);

            var dto = mapper.Map<SizeModelItemDto>(data);

            return dto;
        }

        public async Task<IEnumerable<SizeModelItemDto>> GetItems(string sizeModelId)
        {
            var data = await context.SizeModelItems
                .Where(x => x.SizeModel.Id == sizeModelId)
                .ToListAsync();

            var dto = mapper.Map<IEnumerable<SizeModelItemDto>>(data);

            return dto;
        }
    }
}
