using Carter;
using MediatR;

namespace MyPlanner.Plannings.Api.UseCases.Plan.GetAllPlanItems
{
    public class GetAllPlanItemsController(IMediator mediator) : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/plans/{planId}/items", async (string planId) =>
            {
                var query = new GetAllPlanItemsQuery(planId);

                var result = await mediator.Send(query);

                return Results.Ok(result);

            }).WithTags(Tags.Plan);
        }
    }
}
