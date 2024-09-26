using Microsoft.AspNetCore.Mvc;
using MyPlanner.Plannings.Api.Dtos.Plan;

namespace MyPlanner.Plannings.Api.UseCases.Plan.Command.DeletePlan
{
    public class DeletePlanController : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapDelete("/plans/{planId}", async ([AsParameters] PlanServices service, [FromBody] DeletePlanDto planDto) =>
            {
                var request = new DeletePlanRequest(planDto.PlanId, planDto.UserId);

                var result = await service.Mediator.Send(request);

                return result ? Results.Ok() : Results.BadRequest();

            }).WithTags(Tags.Plan);
        }
    }
}
