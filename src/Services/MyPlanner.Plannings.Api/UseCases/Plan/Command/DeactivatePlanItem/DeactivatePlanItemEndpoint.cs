﻿using MyPlanner.Planning.Models.Plan;

namespace MyPlanner.Plannings.Api.UseCases.Plan.Command.DeactivatePlanItem
{
    public class DeactivatePlanItemEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPut("/plans/{planId}/items/{planItemId}/deactivate", async (
              [FromHeader(Name = "x-requestid")] Guid requestId,
              [AsParameters] PlanServices service, string planId, string planItemId, [FromBody] DeactivatePlanItemDto deactivatePlanItemDto) =>
            {
                if (requestId == Guid.Empty)
                {
                    service.Logger.LogWarning("Invalid IntegrationEvent - RequestId is missing - {@IntegrationEvent}", deactivatePlanItemDto);
                    return TypedResults.BadRequest("RequestId is missing.");
                }

                using (service.Logger.BeginScope(new List<KeyValuePair<string, object>> { new("IdentifiedCommandId", requestId) }))
                {
                    var command = new DeactivatePlanItemCommand(planItemId, deactivatePlanItemDto.UserId);

                    var request = new IdentifiedCommand<DeactivatePlanItemCommand, ResultSet>(command, requestId);

                    var result = await service.Mediator.Send(request);

                    if (result.IsSuccess)
                    {
                        service.Logger.LogInformation("DeactivatePlanItemCommand succeeded - RequestId: {RequestId}", requestId);
                        return Results.Ok(result);
                    }
                    else
                    {
                        service.Logger.LogWarning("DeactivatePlanItemCommand failed - RequestId: {RequestId}", requestId);
                        return Results.BadRequest(result);
                    }
                }
            }).WithTags(Tags.Plan);
        }
    }
}
