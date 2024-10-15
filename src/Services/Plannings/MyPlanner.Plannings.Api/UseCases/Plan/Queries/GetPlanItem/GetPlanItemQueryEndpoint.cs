namespace MyPlanner.Plannings.Api.UseCases.Plan.Queries.GetPlanItem
{
    public class GetPlanItemQueryEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/plans/{planId}/items/{planItemId}", async ([AsParameters] PlanServices service, string planId, string planItemId) =>
            {
                var query = new GetPlanItemQuery(planId, planItemId);

                var result = await service.Mediator.Send(query);

                return Results.Ok(result);

            }).WithTags(Tags.Plan);
        }
    }
}
