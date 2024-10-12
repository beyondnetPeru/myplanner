using MyPlanner.Plannings.Api.UseCases.Plan.Command.ChangeName;

namespace MyPlanner.Plannings.Api.UseCases.Plan.Command.ChangePlanName
{
    public class ChangePlanNameEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPut("/plans/{planId}/changename", async (
                [FromHeader(Name = "x-requestid")] Guid requestId,
                [AsParameters] PlanServices service, string planId, 
                [FromBody] ChangePlanNameDto changePlanNameDto) =>
            {
                if (requestId == Guid.Empty)
                {
                    service.Logger.LogWarning("Invalid IntegrationEvent - RequestId is missing - {@IntegrationEvent}", changePlanNameDto);
                    return TypedResults.BadRequest("RequestId is missing.");
                }

                using (service.Logger.BeginScope(new List<KeyValuePair<string, object>> { new("IdentifiedCommandId", requestId) }))
                { 
                    var command = new ChangePlanNameCommand(planId, changePlanNameDto.Name, changePlanNameDto.UserId);

                    var request = new IdentifiedCommand<ChangePlanNameCommand, ResultSet>(command, requestId);

                    var result = await service.Mediator.Send(request);

                    if (result.IsSuccess)
                    {
                        service.Logger.LogInformation("ChangePlanNameCommand succeeded - RequestId: {RequestId}", requestId);
                        return Results.Ok(result);
                    }
                    else
                    {
                        service.Logger.LogWarning("ChangePlanNameCommand failed - RequestId: {RequestId}", requestId);
                        return Results.BadRequest(result);
                    }

                }
            }).WithTags(Tags.Plan);
        }
    }
}
