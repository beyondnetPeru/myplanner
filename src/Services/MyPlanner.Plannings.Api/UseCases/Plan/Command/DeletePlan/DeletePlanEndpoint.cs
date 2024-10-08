

namespace MyPlanner.Plannings.Api.UseCases.Plan.Command.DeletePlan
{
    public class DeletePlanEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapDelete("/plans/{planId}", async ([AsParameters] PlanServices service, [FromBody] DeletePlanDto planDto) =>
            {
                var request = new DeletePlanCommand(planDto.PlanId, planDto.UserId);

                var result = await service.Mediator.Send(request);

                return result.IsSuccess ? Results.Ok(result) : Results.BadRequest(result);

            }).WithTags(Tags.Plan);
        }
    }
}
