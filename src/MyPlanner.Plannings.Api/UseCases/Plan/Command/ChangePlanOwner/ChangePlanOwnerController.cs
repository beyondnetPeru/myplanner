
using MyPlanner.Plannings.Api.Dtos.Plan;

namespace MyPlanner.Plannings.Api.UseCases.Plan.Command.ChangeOwner
{
    public class ChangePlanOwnerController : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/plans/{planId}/changeowner", async ([AsParameters] PlanServices service, ChangePlanOwnerDto changePlanOwnerDto) =>
            {
                var request = new ChangePlanOwnerRequest(changePlanOwnerDto.Id, changePlanOwnerDto.Owner, changePlanOwnerDto.UserId);

                var result = await service.Mediator.Send(request);

                return result ? Results.Ok() : Results.BadRequest();

            });

        }
    }
}
