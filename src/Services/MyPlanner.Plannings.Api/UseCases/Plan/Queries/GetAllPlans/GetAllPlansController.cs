using MyPlanner.Shared.Models.Pagination;

namespace MyPlanner.Plannings.Api.UseCases.Plan.Queries.GetAllPlans
{
    public class GetAllPlansController : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/plans", async ([AsParameters] PlanServices service, [FromBody] PaginationDto paginationDto) =>
            {
                var pagination = new PaginationQuery(paginationDto.Page, paginationDto.RecordsPerPageMax);

                var query = new GetAllPlansQuery(pagination);

                var result = await service.Mediator.Send(query);

                return result != null ? Results.Ok(result) : Results.NotFound();

            }).WithTags(Tags.Plan);
        }
    }
}
