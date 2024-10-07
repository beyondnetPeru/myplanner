namespace MyPlanner.Plannings.Api.UseCases.Plan.Queries.GetAllPlans
{
    public class GetAllPlansEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/plans", async ([AsParameters] PlanServices service, [FromBody] PaginationDto paginationDto) =>
            {
                var pagination = new PaginationQuery(paginationDto.Page, paginationDto.RecordsPerPageMax);

                var query = new GetAllPlansQuery(pagination);

                var result = await service.Mediator.Send(query);

                return result.IsSuccess ? Results.Ok(result) : Results.NotFound(result);

            }).WithTags(Tags.Plan);
        }
    }
}
