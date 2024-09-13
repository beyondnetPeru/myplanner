using Microsoft.AspNetCore.Mvc;
using MyPlanner.Plannings.Api.Dtos.SizeModelType;

namespace MyPlanner.Plannings.Api.UseCases.SizeModelTypes.ActivateSizeModelTypeFactor
{
    public class ActivateSizeModelTypeFactorController(IMediator mediator) : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPut("/sizemodeltypes/{sizeModelTypeId}/factors/{sizeModelTypeFactorId}/activate", async ([FromHeader(Name = "x-requestid")] Guid requestId,
                                                                             [FromBody] ActivateSizeModelTypeFactorDto activateSizeModelTypeFactorDto) =>
            {
                var request = new ActivateSizeModelTypeFactorRequest(activateSizeModelTypeFactorDto.SizeModelTypeId, activateSizeModelTypeFactorDto.UserId);

                var result = await mediator.Send(request);

                {
                    Results.BadRequest();
                }

                return Results.Ok(result);

            }).WithTags(Tags.SizeModelTypes);
        }
    }
}
