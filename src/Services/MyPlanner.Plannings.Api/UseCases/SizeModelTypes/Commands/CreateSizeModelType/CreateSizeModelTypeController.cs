using Microsoft.AspNetCore.Mvc;
using MyPlanner.Plannings.Api.Dtos.SizeModelType;

namespace MyPlanner.Plannings.Api.UseCases.SizeModelTypes.Commands.CreateSizeModelType
{
    public class CreateSizeModelTypeController : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/sizemodeltypes/", async ([FromHeader(Name = "x-requestid")] Guid requestId,
                                                   [AsParameters] SizeModelTypeService service,
                                                   [FromBody] CreateSizeModelTypeDto createSizeModelTypeDto) =>
            {
                var request = service.Mapper.Map<CreateSizeModelTypeRequest>(createSizeModelTypeDto);

                var result = await service.Mediator.Send(request);

                if (!result)
                {
                    Results.BadRequest();
                }

                return Results.Ok(result);

            }).WithTags(Tags.SizeModelTypes);
        }
    }
}
