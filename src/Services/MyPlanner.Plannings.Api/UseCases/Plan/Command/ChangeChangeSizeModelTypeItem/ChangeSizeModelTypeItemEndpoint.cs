using MyPlanner.Planning.Models.Plan;

namespace MyPlanner.Plannings.Api.UseCases.Plan.Command.ChangeChangeSizeModelTypeItem
{
    public class ChangeSizeModelTypeItemEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPut("/plans/{planId}/items/{planItemId}/changesizemodeltypeitem", async (
              [FromHeader(Name = "x-requestid")] Guid requestId,
              [AsParameters] PlanServices service, string planId, string planItemId, [FromBody] ChangeChangeSizeModelTypeItemDto changeChangeSizeModelTypeItemDto) =>
            {
                if (requestId == Guid.Empty)
                {
                    service.Logger.LogWarning("Invalid IntegrationEvent - RequestId is missing - {@IntegrationEvent}", changeChangeSizeModelTypeItemDto);
                    return TypedResults.BadRequest("RequestId is missing.");
                }

                using (service.Logger.BeginScope(new List<KeyValuePair<string, object>> { new("IdentifiedCommandId", requestId) }))
                {
                    var command = new ChangeSizeModelTypeItemCommand(planId, planItemId, changeChangeSizeModelTypeItemDto.UserId);

                    var request = new IdentifiedCommand<ChangeSizeModelTypeItemCommand, ResultSet>(command, requestId);

                    var result = await service.Mediator.Send(request);

                    if (result.IsSuccess)
                    {
                        service.Logger.LogInformation("RemovePlanItemCommand succeeded - RequestId: {RequestId}", requestId);
                        return Results.Ok(result);
                    }
                    else
                    {
                        service.Logger.LogWarning("RemovePlanItemCommand failed - RequestId: {RequestId}", requestId);
                        return Results.BadRequest(result);
                    }
                }
            }).WithTags(Tags.Plan);
        }
    }
}
