using Microsoft.AspNetCore.Mvc;
using MyPlanner.Plannings.Api.Dtos.SizeModelType;

namespace MyPlanner.Plannings.Api.UseCases.SizeModelTypes.Commands.DeactivateSizeModelType
{
    public class DeactivateSizeModelTypeController : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPut("/sizemodeltypes/{sizeModelTypeId}/deactivate", async ([FromHeader(Name = "x-requestid")] Guid requestId,
                                                                            [AsParameters] SizeModelTypeService service,
                                                                            [FromBody] DeactivateSizeModelTypeDto deactivateSizeModelTypeDto) =>
            {
                var request = service.Mapper.Map<DeactivateSizeModelTypeRequest>(deactivateSizeModelTypeDto);

                var result = await service.Mediator.Send(request);

                {
                    Results.BadRequest();
                }

                return Results.Ok(result);

            }).WithTags(Tags.SizeModelTypes);
        }
    }
}
