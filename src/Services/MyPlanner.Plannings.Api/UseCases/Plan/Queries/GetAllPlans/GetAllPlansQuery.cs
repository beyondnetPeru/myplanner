using MyPlanner.Plannings.Api.Dtos.Plan;
using MyPlanner.Shared.Application.Dtos;

namespace MyPlanner.Plannings.Api.UseCases.Plan.Queries.GetAllPlans
{
    public class GetAllPlansQuery : IRequest<IEnumerable<PlanDto>>
    {
        public GetAllPlansQuery(PaginationDto pagination)
        {
            Pagination = pagination;
        }

        public PaginationDto Pagination { get; }
    }
}
