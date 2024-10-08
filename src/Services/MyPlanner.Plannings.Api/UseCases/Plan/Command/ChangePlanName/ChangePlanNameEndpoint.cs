
using MyPlanner.Plannings.Api.UseCases.Plan.Command.ChangeName;

namespace MyPlanner.Plannings.Api.UseCases.Plan.Command.ChangePlanName
{
    public class ChangePlanNameEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPut("/plans/{planId}/name", async ([AsParameters] PlanServices service, [FromBody] ChangePlanNameDto changePlanNameDto) =>
            {
                var request = new ChangePlanNameCommand(changePlanNameDto.PlanId, changePlanNameDto.Name, changePlanNameDto.UserId);

                var result = await service.Mediator.Send(request);

                return result.IsSuccess ? Results.Ok(result) : Results.BadRequest(result);  

            }).WithTags(Tags.Plan);
        }
    }
}
