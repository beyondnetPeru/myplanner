using MyPlanner.Planning.Models.Plan;

namespace MyPlanner.Plannings.Api.UseCases.Plan.Command.ChangePlanItemType
{
    public class ChangePlanItemTypeEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPut("/plans/{planId}/items/{planItemId}/changeplanitemtype", async (
                [FromHeader(Name = "x-requestid")] Guid requestId,
                [AsParameters] PlanServices service,
                string planId,
                string planItemId,
                [FromBody] ChangePlanItemTypeDto changePlanItemTypeDto) =>
            {
                if (requestId == Guid.Empty)
                {
                    service.Logger.LogWarning("Invalid IntegrationEvent - RequestId is missing - {@IntegrationEvent}", changePlanItemTypeDto);
                    return TypedResults.BadRequest("RequestId is missing.");
                }

                using (service.Logger.BeginScope(new List<KeyValuePair<string, object>> { new("IdentifiedCommandId", requestId) }))
                {
                    var command = new ChangePlanItemTypeCommand(planItemId, changePlanItemTypeDto.PlanItemType, changePlanItemTypeDto.UserId);

                    var request = new IdentifiedCommand<ChangePlanItemTypeCommand, ResultSet>(command, requestId);

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