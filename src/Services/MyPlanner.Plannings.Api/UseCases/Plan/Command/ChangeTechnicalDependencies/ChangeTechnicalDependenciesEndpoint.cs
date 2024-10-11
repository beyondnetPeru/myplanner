using MyPlanner.Planning.Models.Plan;
using MyPlanner.Shared.Cqrs;

namespace MyPlanner.Plannings.Api.UseCases.Plan.Command.ChangeTechnicalDependencies
{
    public class ChangeTechnicalDependenciesEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPut("/plans/{planId}/items/{planItemId}/changetechnicaldependencies", async (
            [FromHeader(Name = "x-requestid")] Guid requestId,
            [AsParameters] PlanServices service, string planId, string planItemId, [FromBody] ChangeTechnicalDependenciesDto changeTechnicalDependenciesDto) =>
            {
                if (requestId == Guid.Empty)
                {
                    service.Logger.LogWarning("Invalid IntegrationEvent - RequestId is missing - {@IntegrationEvent}", changeTechnicalDependenciesDto);
                    return TypedResults.BadRequest("RequestId is missing.");
                }

                using (service.Logger.BeginScope(new List<KeyValuePair<string, object>> { new("IdentifiedCommandId", requestId) }))
                {
                    var command = new ClosePlanItemCommand(planId, planItemId, changeTechnicalDependenciesDto.UserId);

                    var request = new IdentifiedCommand<ClosePlanItemCommand, ResultSet>(command, requestId);

                    var result = await service.Mediator.Send(request);

                    if (result.IsSuccess)
                    {
                        service.Logger.LogInformation("ChangeTechnicalDependenciesCommand succeeded - RequestId: {RequestId}", requestId);
                        return Results.Ok(result);
                    }
                    else
                    {
                        service.Logger.LogWarning("ChangeTechnicalDependenciesCommand failed - RequestId: {RequestId}", requestId);
                        return Results.BadRequest(result);
                    }
                }
            }).WithTags(Tags.Plan);
        }
    }
}
