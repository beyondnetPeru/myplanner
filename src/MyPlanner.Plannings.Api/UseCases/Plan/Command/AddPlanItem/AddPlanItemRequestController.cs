
using Microsoft.AspNetCore.Mvc;
using MyPlanner.Plannings.Api.Dtos.Plan;

namespace MyPlanner.Plannings.Api.UseCases.Plan.Command.AddPlanItem
{
    public class AddPlanItemRequestController : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/plans/{planId}/items", async ([AsParameters] PlanServices service, [FromBody] AddPlanItemDto planItem) =>
            {
                var request = new AddPlanItemRequest(
                    planItem.PlanId,
                    planItem.CategoryName,
                    planItem.Name,
                    planItem.BusinessDefinition,
                    planItem.ComplexityLevel,
                    planItem.BacklogName,
                    planItem.Priority,
                    planItem.MoScoW,
                    planItem.SizeModelTypeFactorId,
                    planItem.SizeModelTypeValueSelected,
                    planItem.BusinessFeature,
                    planItem.TechnicalDefinition,
                    planItem.ComponentsImpacted,
                    planItem.TechnicalDependencies,
                    planItem.BallParkCost,
                    planItem.BallParkDependenciesCost,
                    planItem.KeyAssumptions,
                    planItem.UserId
                    );

                var result = await service.Mediator.Send(request);

                return result ? Results.Ok() : Results.BadRequest();

            }).WithTags(Tags.Plan);
        }
    }
}
