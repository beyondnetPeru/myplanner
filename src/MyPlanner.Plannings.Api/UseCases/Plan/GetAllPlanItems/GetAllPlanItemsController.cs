namespace MyPlanner.Plannings.Api.UseCases.Plan.GetAllPlanItems
{
    public class GetAllPlanItemsController : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/plans/{planId}/items", async ([AsParameters] PlanServices service, string planId) =>
            {
                var query = new GetAllPlanItemsQuery(planId);

                var result = await service.Mediator.Send(query);

                return Results.Ok(result);

            }).WithTags(Tags.Plan);
        }
    }
}
