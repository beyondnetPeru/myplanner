using MyPlanner.Plannings.Api.Dtos.Plan;

namespace MyPlanner.Plannings.Api.UseCases.Plan.Command.CreatePlan
{
    public class CreatePlanController : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/plans/", async ([FromHeader(Name = "x-requestid")] Guid requestId, 
                                          [AsParameters] PlanServices service, 
                                          [FromBody] CreatePlanDto createPlanDto) =>
            {
                var request = service.Mapper.Map<AddPlanItemRequest>(createPlanDto);

                var result = await service.Mediator.Send(request);

                return result ? Results.Ok() : Results.BadRequest();

            }).WithTags(Tags.Plan);
        }
    }
}
