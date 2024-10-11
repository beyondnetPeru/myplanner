using MyPlanner.Planning.Models.Plan;
using MyPlanner.Plannings.Api.UseCases.Plan.Command.ChangeTechnicalDefinitionPlanItem;
using MyPlanner.Shared.Cqrs;

namespace MyPlanner.Plannings.Api.UseCases.Plan.Command.ChangeTechnicalDefinition
{
    public class ChangeTechnicalDefinitionEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPut("/plans/{planId}/items/{planItemId}/changetechnicaldefinition", async (
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
                    var command = new ChangeTechnicalDefinitionCommand(planId, planItemId, changeChangeSizeModelTypeItemDto.UserId);

                    var request = new IdentifiedCommand<ChangeTechnicalDefinitionCommand, ResultSet>(command, requestId);

                    var result = await service.Mediator.Send(request);

                    if (result.IsSuccess)
                    {
                        service.Logger.LogInformation("ChangeTechnicalDefinitionCommand succeeded - RequestId: {RequestId}", requestId);
                        return Results.Ok(result);
                    }
                    else
                    {
                        service.Logger.LogWarning("ChangeTechnicalDefinitionCommand failed - RequestId: {RequestId}", requestId);
                        return Results.BadRequest(result);
                    }
                }
            }).WithTags(Tags.Plan);
        }
    }
}
