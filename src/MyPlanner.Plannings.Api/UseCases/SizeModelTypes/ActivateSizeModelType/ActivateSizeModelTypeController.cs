using Microsoft.AspNetCore.Mvc;
using MyPlanner.Plannings.Api.Dtos.SizeModelType;

namespace MyPlanner.Plannings.Api.UseCases.SizeModelTypes.ActivateSizeModelType
{
    public class ActivateSizeModelTypeController : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPut("/sizemodeltypes/{sizeModelTypeId}/activate", async ([FromHeader(Name = "x-requestid")] Guid requestId,
                                                                             [AsParameters] SizeModelTypeService service,
                                                                             [FromBody] ActivateSizeModelTypeDto activateSizeModelTypeDto) =>
            {
                var request = new ActivateSizeModelTypeRequest(activateSizeModelTypeDto.SizeModelTypeId, activateSizeModelTypeDto.UserId);

                var result = await service.Mediator.Send(request);

                {
                    Results.BadRequest();
                }

                return Results.Ok(result);


            }).WithTags(Tags.SizeModelTypes);
        }

    }
}
