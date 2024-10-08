

namespace MyPlanner.Plannings.Api.UseCases.Plan.Command.DeactivatePlan
{
    public class DeactivatePlanEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPut("/plans/{planId}/deactivate", async ([AsParameters] PlanServices service, [FromBody] DeactivatePlanDto planDto) =>
            {
                var request = new DeactivatePlanCommand(planDto.PlanId, planDto.UserId);

                var result = await service.Mediator.Send(request);

                return result.IsSuccess ? Results.Ok(result) : Results.BadRequest(result);

            }).WithTags(Tags.Plan);
        }
    }
}
