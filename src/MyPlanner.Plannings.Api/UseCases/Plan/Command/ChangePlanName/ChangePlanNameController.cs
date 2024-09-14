using Microsoft.AspNetCore.Mvc;
using MyPlanner.Plannings.Api.Dtos.Plan;
using MyPlanner.Plannings.Api.UseCases.Plan.Command.ChangeName;

namespace MyPlanner.Plannings.Api.UseCases.Plan.Command.ChangePlanName
{
    public class ChangePlanNameController : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPut("/plans/{planId}/name", async ([AsParameters] PlanServices service, [FromBody] ChangePlanNameDto changePlanNameDto) =>
            {
                var request = new ChangePlanNameRequest(changePlanNameDto.PlanId, changePlanNameDto.Name);

                await service.Mediator.Send(request);

                return Results.Ok(true);

            }).WithTags(Tags.Plan);
        }
    }
}
