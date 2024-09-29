using MyPlanner.Shared.Models.Pagination.Dtos;

namespace MyPlanner.Plannings.Api.UseCases.Plan.Queries.GetAllPlans
{
    public class GetAllPlansController : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/plans", async ([AsParameters] PlanServices service, int page = 0, int recordsPerPage = 10) =>
            {
                var pagination = new PaginationDto()
                {
                    Page = page,
                    RecordsPerPage = recordsPerPage
                };

                var query = new GetAllPlansQuery(pagination);

                var result = await service.Mediator.Send(query);

                return result != null ? Results.Ok(result) : Results.NotFound();

            }).WithTags(Tags.Plan);
        }
    }
}
