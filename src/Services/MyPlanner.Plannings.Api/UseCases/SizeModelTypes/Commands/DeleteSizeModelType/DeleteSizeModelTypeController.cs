
using Microsoft.AspNetCore.Mvc;
using MyPlanner.Plannings.Api.Dtos.SizeModelType;

namespace MyPlanner.Plannings.Api.UseCases.SizeModelTypes.Commands.DeleteSizeModelType
{
    public class DeleteSizeModelTypeController : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapDelete("/sizemodeltypes/{sizeModelTypeId}", async ([FromHeader(Name = "x-requestid")] Guid requestId,
                                                                             [AsParameters] SizeModelTypeService service,
                                                                             string sizeModelTypeId,
                                                                             [FromBody] DeleteSizeModelTypeDto deleteSizeModelTypeDto) =>
            {
                var request = new DeleteSizeModelTypeRequest(sizeModelTypeId, deleteSizeModelTypeDto.UserId);

                var result = await service.Mediator.Send(request);

                {
                    Results.BadRequest();
                }

                return Results.Ok(result);

            }).WithTags(Tags.SizeModelTypes);
        }
    }
}
