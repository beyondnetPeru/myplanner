

namespace MyPlanner.Plannings.Api.UseCases.Plan.Command.ChangeOwner
{
    public class ChangePlanOwnerEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPut("/plans/{planId}/changeowner", async ([AsParameters] PlanServices service, [FromBody] ChangePlanOwnerDto changePlanOwnerDto) =>
            {
                var request = new ChangePlanOwnerCommand(changePlanOwnerDto.Id, changePlanOwnerDto.Owner, changePlanOwnerDto.UserId);

                var result = await service.Mediator.Send(request);

                return result.IsSuccess ? Results.Ok(result) : Results.BadRequest(result);

            }).WithTags(Tags.Plan);

        }
    }
}
