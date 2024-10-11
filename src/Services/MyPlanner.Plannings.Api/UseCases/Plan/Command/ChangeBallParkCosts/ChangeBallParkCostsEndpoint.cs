﻿using MyPlanner.Planning.Models.Plan;
using MyPlanner.Shared.Cqrs;

namespace MyPlanner.Plannings.Api.UseCases.Plan.Command.ChangeBallParkCosts
{
    public class ChangeBallParkCostsEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPut("/plans/{planId}/items/{planItemId}/changeballparkcosts", async (
              [FromHeader(Name = "x-requestid")] Guid requestId,
              [AsParameters] PlanServices service, string planId, string planItemId, [FromBody] ChangeBallParkCostsDto changeBallParkCostsDto) =>
            {
                if (requestId == Guid.Empty)
                {
                    service.Logger.LogWarning("Invalid IntegrationEvent - RequestId is missing - {@IntegrationEvent}", changeBallParkCostsDto);
                    return TypedResults.BadRequest("RequestId is missing.");
                }

                using (service.Logger.BeginScope(new List<KeyValuePair<string, object>> { new("IdentifiedCommandId", requestId) }))
                {
                    var command = new ChangeBallParkCostsCommand(planItemId, changeBallParkCostsDto.CurrencySymbol, changeBallParkCostsDto.BallParkCost, changeBallParkCostsDto.BallparkDependenciesCost, changeBallParkCostsDto.UserId);

                    var request = new IdentifiedCommand<ChangeBallParkCostsCommand, ResultSet>(command, requestId);

                    var result = await service.Mediator.Send(request);

                    if (result.IsSuccess)
                    {
                        service.Logger.LogInformation("ChangeBallParkCostsCommand succeeded - RequestId: {RequestId}", requestId);
                        return Results.Ok(result);
                    }
                    else
                    {
                        service.Logger.LogWarning("ChangeBallParkCostsCommand failed - RequestId: {RequestId}", requestId);
                        return Results.BadRequest(result);
                    }
                }
            }).WithTags(Tags.Plan);
        }
    }
}