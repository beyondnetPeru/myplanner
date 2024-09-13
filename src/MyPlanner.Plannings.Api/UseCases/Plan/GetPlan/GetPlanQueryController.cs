namespace MyPlanner.Plannings.Api.UseCases.Plan.GetPlan
{
    public class GetPlanQueryController(IMediator mediator) : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/plans/{planId}", async (string planId) =>
            {
                var query = new GetPlanQuery(planId);

                var result = await mediator.Send(query);

                return Results.Ok(result);

            }).WithTags(Tags.Plan);
        }
    }
}
