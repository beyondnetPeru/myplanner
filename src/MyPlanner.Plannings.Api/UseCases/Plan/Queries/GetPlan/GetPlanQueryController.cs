namespace MyPlanner.Plannings.Api.UseCases.Plan.Queries.GetPlan
{
    public class GetPlanQueryController : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/plans/{planId}", async ([AsParameters] PlanServices services, string planId) =>
            {
                var query = new GetPlanQuery(planId);

                var result = await services.Mediator.Send(query);

                return Results.Ok(result);

            }).WithTags(Tags.Plan);
        }
    }
}
