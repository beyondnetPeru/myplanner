using MediatR;
using MyPlanner.Plannings.Api.Dtos.Plan;

namespace MyPlanner.Plannings.Api.UseCases.Plan.GetAllPlanItems
{
    public class GetAllPlanItemsQuery : IRequest<IEnumerable<PlanItemDto>>
    {
        public string PlanId { get; set; }

        public GetAllPlanItemsQuery(string planId)
        {
            PlanId = planId;
        }
    }
}
