using MyPlanner.Planning.Models.Plan;

namespace MyPlanner.Plannings.Api.UseCases.Plan.Command.ChangeAssumptions
{
    public class ChangeAssumptionsEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPut("/plans/{planId}/items/{planItemId}/changeassumptions", async (
             [FromHeader(Name = "x-requestid")] Guid requestId,
             [AsParameters] PlanServices service, string planId, string planItemId, [FromBody] ChangeAssumptionsDto changeAssumptionsDto) =>
            {
                if (requestId == Guid.Empty)
                {
                    service.Logger.LogWarning("Invalid IntegrationEvent - RequestId is missing - {@IntegrationEvent}", changeAssumptionsDto);
                    return TypedResults.BadRequest("RequestId is missing.");
                }

                using (service.Logger.BeginScope(new List<KeyValuePair<string, object>> { new("IdentifiedCommandId", requestId) }))
                {
                    var command = new ChangeAssumptionsCommand(planItemId, changeAssumptionsDto.Assumptions, changeAssumptionsDto.UserId);

                    var request = new IdentifiedCommand<ChangeAssumptionsCommand, ResultSet>(command, requestId);

                    var result = await service.Mediator.Send(request);

                    if (result.IsSuccess)
                    {
                        service.Logger.LogInformation("ChangeAssumptionsCommand succeeded - RequestId: {RequestId}", requestId);
                        return Results.Ok(result);
                    }
                    else
                    {
                        service.Logger.LogWarning("ChangeAssumptionsCommand failed - RequestId: {RequestId}", requestId);
                        return Results.BadRequest(result);
                    }
                }
            }).WithTags(Tags.Plan);
        }
    }
}
