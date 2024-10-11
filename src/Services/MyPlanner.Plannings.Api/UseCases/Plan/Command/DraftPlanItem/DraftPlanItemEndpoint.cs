using MyPlanner.Planning.Models.Plan;
using MyPlanner.Shared.Cqrs;

namespace MyPlanner.Plannings.Api.UseCases.Plan.Command.DraftPlanItem
{
    public class DraftPlanItemEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPut("/plans/{planId}/items/{planItemId}/draft", async (
               [FromHeader(Name = "x-requestid")] Guid requestId,
               [AsParameters] PlanServices service, string planId, string planItemId, [FromBody] DraftPlanItemDto draftPlanItemDto) =>
            {
                if (requestId == Guid.Empty)
                {
                    service.Logger.LogWarning("Invalid IntegrationEvent - RequestId is missing - {@IntegrationEvent}", draftPlanItemDto);
                    return TypedResults.BadRequest("RequestId is missing.");
                }

                using (service.Logger.BeginScope(new List<KeyValuePair<string, object>> { new("IdentifiedCommandId", requestId) }))
                {
                    var command = new DraftPlanItemCommand(planItemId, draftPlanItemDto.UserId);

                    var request = new IdentifiedCommand<DraftPlanItemCommand, ResultSet>(command, requestId);

                    var result = await service.Mediator.Send(request);

                    if (result.IsSuccess)
                    {
                        service.Logger.LogInformation("DraftPlanItemCommand succeeded - RequestId: {RequestId}", requestId);
                        return Results.Ok(result);
                    }
                    else
                    {
                        service.Logger.LogWarning("DraftPlanItemCommand failed - RequestId: {RequestId}", requestId);
                        return Results.BadRequest(result);
                    }
                }
            }).WithTags(Tags.Plan);
        }
    }
}
