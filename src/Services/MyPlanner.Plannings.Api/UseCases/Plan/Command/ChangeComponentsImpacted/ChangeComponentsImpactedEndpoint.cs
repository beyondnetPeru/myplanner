using MyPlanner.Planning.Models.Plan;
using MyPlanner.Shared.Cqrs;

namespace MyPlanner.Plannings.Api.UseCases.Plan.Command.ChangeComponentsImpacted
{
    public class ChangeComponentsImpactedEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPut("/plans/{planId}/items/{planItemId}/changecomponentsimpacted", async (
            [FromHeader(Name = "x-requestid")] Guid requestId,
            [AsParameters] PlanServices service, string planId, string planItemId, [FromBody] ChangeComponentsImpactedDto changeComponentsImpactedDto) =>
            {
                if (requestId == Guid.Empty)
                {
                    service.Logger.LogWarning("Invalid IntegrationEvent - RequestId is missing - {@IntegrationEvent}", changeComponentsImpactedDto);
                    return TypedResults.BadRequest("RequestId is missing.");
                }

                using (service.Logger.BeginScope(new List<KeyValuePair<string, object>> { new("IdentifiedCommandId", requestId) }))
                {
                    var command = new ChangeComponentsImpactedCommand(planId, planItemId, changeComponentsImpactedDto.UserId);

                    var request = new IdentifiedCommand<ChangeComponentsImpactedCommand, ResultSet>(command, requestId);

                    var result = await service.Mediator.Send(request);

                    if (result.IsSuccess)
                    {
                        service.Logger.LogInformation("ChangeComponentsImpactedCommand succeeded - RequestId: {RequestId}", requestId);
                        return Results.Ok(result);
                    }
                    else
                    {
                        service.Logger.LogWarning("ChangeComponentsImpactedCommand failed - RequestId: {RequestId}", requestId);
                        return Results.BadRequest(result);
                    }
                }
            }).WithTags(Tags.Plan);
        }
    }
}
