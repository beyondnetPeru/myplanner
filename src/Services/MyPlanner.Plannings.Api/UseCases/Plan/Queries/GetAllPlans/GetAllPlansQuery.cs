using MyPlanner.Plannings.Api.Dtos.Plan;
using MyPlanner.Shared.Models.Pagination;

namespace MyPlanner.Plannings.Api.UseCases.Plan.Queries.GetAllPlans
{
    public class GetAllPlansQuery : IRequest<IEnumerable<PlanDto>>
    {
        public GetAllPlansQuery(PaginationQuery pagination)
        {
            Pagination = pagination;
        }

        public PaginationQuery Pagination { get; }
    }
}
