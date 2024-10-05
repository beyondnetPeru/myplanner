using MyPlanner.Shared.Cqrs;
using MyPlanner.Shared.Cqrs.Interfaces;

namespace MyPlanner.Plannings.Api.UseCases.Plan.Queries.GetAllPlans
{
    public class GetAllPlansQuery : IQuery<ResultSet>
    {
        public GetAllPlansQuery(PaginationQuery pagination)
        {
            Pagination = pagination;
        }

        public PaginationQuery Pagination { get; }
    }
}
