using MyPlanner.Planning.Models.Plan;

namespace MyPlanner.Plannings.Api.UseCases.Plan.Command.ClosePlanItem
{
    public class ClosePlanItemEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPut("/plans/{planId}/items/{planItemId}/close", async (
             [FromHeader(Name = "x-requestid")] Guid requestId,
             [AsParameters] PlanServices service, string planId, string planItemId, [FromBody] ClosePlanItemDto closePlanItemDto) =>
            {
                if (requestId == Guid.Empty)
                {
                    service.Logger.LogWarning("Invalid IntegrationEvent - RequestId is missing - {@IntegrationEvent}", closePlanItemDto);
                    return TypedResults.BadRequest("RequestId is missing.");
                }

                using (service.Logger.BeginScope(new List<KeyValuePair<string, object>> { new("IdentifiedCommandId", requestId) }))
                {
                    var command = new ClosePlanItemCommand(planItemId, closePlanItemDto.UserId);

                    var request = new IdentifiedCommand<ClosePlanItemCommand, ResultSet>(command, requestId);

                    var result = await service.Mediator.Send(request);

                    if (result.IsSuccess)
                    {
                        service.Logger.LogInformation("ClosePlanItemCommand succeeded - RequestId: {RequestId}", requestId);
                        return Results.Ok(result);
                    }
                    else
                    {
                        service.Logger.LogWarning("ClosePlanItemCommand failed - RequestId: {RequestId}", requestId);
                        return Results.BadRequest(result);
                    }
                }
            }).WithTags(Tags.Plan);
        }
    }
}
