﻿namespace MyPlanner.Plannings.Api.UseCases.Plan.Command.CreatePlan
{
    public class CreatePlanEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/plans/", async ([FromHeader(Name = "x-requestid")] Guid requestId,
                                         [AsParameters] PlanServices service,
                                         [FromBody] CreatePlanDto createPlanDto) =>
            {
                if (requestId == Guid.Empty)
                {
                    service.Logger.LogWarning("Invalid IntegrationEvent - RequestId is missing - {@IntegrationEvent}", createPlanDto);
                    return TypedResults.BadRequest("RequestId is missing.");
                }

                using (service.Logger.BeginScope(new List<KeyValuePair<string, object>> { new("IdentifiedCommandId", requestId) }))
                {
                    var command = service.Mapper.Map<CreatePlanCommand>(createPlanDto);

                    var requestCreatePlan = new IdentifiedCommand<CreatePlanCommand, ResultSet>(command, requestId);

                    var result = await service.Mediator.Send(requestCreatePlan);

                    if (result.IsSuccess)
                    {
                        service.Logger.LogInformation("CreatePlanCommand succeeded - RequestId: {RequestId}", requestId);
                        return Results.Ok(result);
                    }
                    else
                    {
                        service.Logger.LogWarning("CreatePlanCommand failed - RequestId: {RequestId}", requestId);
                        return Results.BadRequest(result);
                    }
                }
            }).WithTags(Tags.Plan); ;
        }
    }
}
