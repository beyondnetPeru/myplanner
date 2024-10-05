﻿using Microsoft.AspNetCore.Mvc;
using MyPlanner.Plannings.Api.Dtos.Plan;

namespace MyPlanner.Plannings.Api.UseCases.Plan.Command.DraftPlan
{
    public class DraftPlanController : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPut("/plans/{planId}/draft", async ([AsParameters] PlanServices service, [FromBody] DraftPlanDto planDto) =>
            {
                var request = new DraftPlanRequest(planDto.PlanId, planDto.UserId);

                var result = await service.Mediator.Send(request);

                return result ? Results.Ok() : Results.BadRequest();

            }).WithTags(Tags.Plan);
        }
    }
}