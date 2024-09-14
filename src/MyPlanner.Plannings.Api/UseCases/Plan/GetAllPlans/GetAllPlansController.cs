using MyPlanner.Plannings.Shared.Application.Dtos;

namespace MyPlanner.Plannings.Api.UseCases.Plan.GetAllPlans
{
    public class GetAllPlansController : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/plans", async ([AsParameters] PlanServices service, int page = 1, int recordsPerPage = 10) =>
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
