using MyPlanner.Plannings.Api.Dtos.Plan;

namespace MyPlanner.Plannings.Api.UseCases.Plan.Queries
{
    public interface IPlanQueryRepository
    {
        Task<IEnumerable<PlanDto>> GetPlans(PaginationQuery pagination);
        Task<PlanDto> GetPlanById(string planId);
        Task<IEnumerable<PlanItemDto>> GetPlanItems(string planId);
        Task<PlanItemDto> GetPlanItem(string planId, string planItemId);
    }
}
