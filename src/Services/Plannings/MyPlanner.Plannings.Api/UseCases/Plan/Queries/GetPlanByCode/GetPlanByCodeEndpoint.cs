namespace MyPlanner.Plannings.Api.UseCases.Plan.Queries.GetPlanByCode
{
    public class GetPlanByCodeEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/plans/bycode/{code}", async ([AsParameters] PlanServices services, string code) =>
            {
                var query = new GetPlanByCodeQuery(code);

                var result = await services.Mediator.Send(query);

                return Results.Ok(result);

            }).WithTags(Tags.Plan);
        }
    }
}
