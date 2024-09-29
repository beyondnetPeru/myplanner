using MyPlanner.Plannings.Api.Dtos.Plan;
using MyPlanner.Shared.Models.Pagination.Dtos;

namespace MyPlanner.Plannings.Api.UseCases.Plan.Queries
{
    public interface IPlanQueryRepository
    {
        Task<IEnumerable<PlanDto>> GetPlans(PaginationDto pagination);
        Task<PlanDto> GetPlanById(string planId);
        Task<IEnumerable<PlanItemDto>> GetPlanItems(string planId);
        Task<PlanItemDto> GetPlanItem(string planId, string planItemId);
    }
}
