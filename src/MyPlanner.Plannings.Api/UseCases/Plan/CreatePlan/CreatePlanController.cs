using AutoMapper;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using MyPlanner.Plannings.Api.Dtos.Plan;

namespace MyPlanner.Plannings.Api.UseCases.Plan.CreatePlan
{
    public class CreatePlanController(IMediator mediator, IMapper mapper) : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/plans/", async ([FromHeader(Name = "x-requestid")] Guid requestId, [FromBody] CreatePlanDto createPlanDto) =>
            {
                var request = mapper.Map<CreatePlanRequest>(createPlanDto);

                var result = await mediator.Send(request);

                return result ? Results.Ok() : Results.BadRequest();

            }).WithTags(Tags.Plan);
        }
    }
}
