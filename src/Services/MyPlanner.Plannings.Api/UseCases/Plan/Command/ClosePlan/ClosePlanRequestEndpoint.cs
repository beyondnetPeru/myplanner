using MyPlanner.Plannings.Api.Dtos.Plan;

namespace MyPlanner.Plannings.Api.UseCases.Plan.Command.ClosePlan
{
    public class ClosePlanRequestEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPut("/plans/{planId}/close", async ([AsParameters] PlanServices service, [FromBody] ClosePlanDto planDto) =>
            {
                var request = new ClosePlanCommand(planDto.PlanId, planDto.UserId);

                var result = await service.Mediator.Send(request);

                return result.IsSuccess ? Results.Ok(result) : Results.BadRequest(result);

            }).WithTags(Tags.Plan);
        }
    }
}
