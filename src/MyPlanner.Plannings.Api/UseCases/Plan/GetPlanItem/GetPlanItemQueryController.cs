using MediatR;
using MyPlanner.Plannings.Api.UseCases;

namespace MyPlanner.Plannings.Api.UseCases.Plan.GetPlanItem
{
    public class GetPlanItemQueryController(IMediator mediator) : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/plans/{planId}/items/{planItemId}", async ([AsParameters] PlanItemServices planIteServices, string planId, string planItemId) =>
            {
                var query = new GetPlanItemQuery(planId, planItemId);

                var result = await mediator.Send(query);

                return Results.Ok(result);

            }).WithTags(Tags.Plan);
        }
    }
}
