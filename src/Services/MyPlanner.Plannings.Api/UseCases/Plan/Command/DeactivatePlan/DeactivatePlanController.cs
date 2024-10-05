﻿using MyPlanner.Plannings.Api.Dtos.Plan;

namespace MyPlanner.Plannings.Api.UseCases.Plan.Command.DeactivatePlan
{
    public class DeactivatePlanController : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPut("/plans/{planId}/deactivate", async ([AsParameters] PlanServices service, [FromBody] DeactivatePlanDto planDto) =>
            {
                var request = new DeactivatePlanRequest(planDto.PlanId, planDto.UserId);

                var result = await service.Mediator.Send(request);

                return result.IsSuccess ? Results.Ok(result) : Results.BadRequest(result);

            }).WithTags(Tags.Plan);
        }
    }
}
