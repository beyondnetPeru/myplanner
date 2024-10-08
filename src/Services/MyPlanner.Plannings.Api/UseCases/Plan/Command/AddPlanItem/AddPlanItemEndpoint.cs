
using MyPlanner.Plannings.Api.UseCases.Plan.Command.CreatePlan;

namespace MyPlanner.Plannings.Api.UseCases.Plan.Command.AddPlanItem
{
    public class AddPlanItemEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/plans/{planId}/items", async ([AsParameters] PlanServices service, [FromBody] AddPlanItemDto planItem) =>
            {
                var request = service.Mapper.Map<AddPlanItemCommand>(planItem);

                var result = await service.Mediator.Send(request);

                return result.IsSuccess ? Results.Ok(result) : Results.BadRequest(result);

            }).WithTags(Tags.Plan);
        }
    }
}
