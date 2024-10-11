using MyPlanner.Planning.Models.Plan;
using MyPlanner.Shared.Cqrs;

namespace MyPlanner.Plannings.Api.UseCases.Plan.Command.ActivatePlanItem
{
    public class ActivatePlanItemEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPut("/plans/{planId}/items/{planItemId}/activate", async (
              [FromHeader(Name = "x-requestid")] Guid requestId,
              [AsParameters] PlanServices service, string planId, string planItemId, [FromBody] ActivatePlanItemDto activatePlanItemDto) =>
            {
                if (requestId == Guid.Empty)
                {
                    service.Logger.LogWarning("Invalid IntegrationEvent - RequestId is missing - {@IntegrationEvent}", activatePlanItemDto);
                    return TypedResults.BadRequest("RequestId is missing.");
                }

                using (service.Logger.BeginScope(new List<KeyValuePair<string, object>> { new("IdentifiedCommandId", requestId) }))
                {
                    var command = new ActivatePlanItemCommand(planItemId, activatePlanItemDto.UserId);

                    var request = new IdentifiedCommand<ActivatePlanItemCommand, ResultSet>(command, requestId);

                    var result = await service.Mediator.Send(request);

                    if (result.IsSuccess)
                    {
                        service.Logger.LogInformation("ActivatePlanItemCommand succeeded - RequestId: {RequestId}", requestId);
                        return Results.Ok(result);
                    }
                    else
                    {
                        service.Logger.LogWarning("ActivatePlanItemCommand failed - RequestId: {RequestId}", requestId);
                        return Results.BadRequest(result);
                    }
                }
            }).WithTags(Tags.Plan);
        }
    }
}
