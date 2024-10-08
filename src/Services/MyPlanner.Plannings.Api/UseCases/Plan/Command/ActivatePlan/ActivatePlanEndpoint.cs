namespace MyPlanner.Plannings.Api.UseCases.Plan.Command.ActivatePlan
{
    public class ActivatePlanEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPut("/plans/{planId}/activate", async ([AsParameters] PlanServices service, [FromBody] ActivatePlanDto planDto) =>
            {
                var request = new ActivatePlanCommand(planDto.PlanId, planDto.UserId);

                var result = await service.Mediator.Send(request);

                return result.IsSuccess ? Results.Ok(result) : Results.BadRequest(result);

            }).WithTags(Tags.Plan);
        }
    }
}
