﻿
using Microsoft.AspNetCore.Mvc;
using MyPlanner.Plannings.Api.Dtos.Plan;

namespace MyPlanner.Plannings.Api.UseCases.Plan.Command.ChangeOwner
{
    public class ChangePlanOwnerController : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPut("/plans/{planId}/changeowner", async ([AsParameters] PlanServices service, [FromBody] ChangePlanOwnerDto changePlanOwnerDto) =>
            {
                var request = new ChangePlanOwnerRequest(changePlanOwnerDto.Id, changePlanOwnerDto.Owner, changePlanOwnerDto.UserId);

                var result = await service.Mediator.Send(request);

                return result ? Results.Ok() : Results.BadRequest();

            }).WithTags(Tags.Plan);

        }
    }
}