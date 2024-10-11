namespace MyPlanner.Plannings.Api.UseCases.Plan.Queries.GetAllPlans
{
    public class GetAllPlansEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/plans", async ([AsParameters] PlanServices service, int page = 1, int recordsPerPage = 5) =>
            {
                var pagination = new PaginationQuery(page, recordsPerPage);

                var query = new GetAllPlansQuery(pagination);

                var result = await service.Mediator.Send(query);

                return result.IsSuccess ? Results.Ok(result) : Results.NotFound(result);

            }).WithTags(Tags.Plan);
        }
    }
}
