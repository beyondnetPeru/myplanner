using Microsoft.EntityFrameworkCore;
using MyPlanner.Plannings.Api.Dtos.Plan;
using MyPlanner.Plannings.Infrastructure.Database;
using MyPlanner.Shared.Application.Dtos;

namespace MyPlanner.Plannings.Api.UseCases.Plan.Queries
{
    public class PlanQueryRepository : IPlanQueryRepository
    {
        private readonly PlanningDbContext context;
        private readonly IMapper mapper;

        public PlanQueryRepository(PlanningDbContext context, IMapper mapper)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<PlanDto> GetPlanById(string planId)
        {
            var plan = await context.Plans.FirstOrDefaultAsync(p => p.Id == planId);

            var dto = mapper.Map<PlanDto>(plan);

            return dto;
        }

        public async Task<PlanItemDto> GetPlanItem(string planId, string planItemId)
        {
            var planItem = await context.PlanItems.FirstOrDefaultAsync(p => p.Plan.Id == planId && p.Id == planItemId);

            var dto = mapper.Map<PlanItemDto>(planItem);

            return dto;
        }

        public async Task<IEnumerable<PlanItemDto>> GetPlanItems(string planId)
        {
            var planItems = await context.PlanItems.Where(p => p.Plan.Id == planId).ToListAsync();

            var dtos = mapper.Map<IEnumerable<PlanItemDto>>(planItems);

            return dtos;
        }

        public async Task<IEnumerable<PlanDto>> GetPlans(PaginationDto pagination)
        {
            var plans = await context.Plans.Skip(pagination.RecordsPerPage).Take(pagination.Page).ToListAsync();

            var dtos = mapper.Map<IEnumerable<PlanDto>>(plans);

            return dtos;
        }
    }
}
