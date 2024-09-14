using Microsoft.AspNetCore.Mvc;
using MyPlanner.Plannings.Api.Dtos.SizeModelType;

namespace MyPlanner.Plannings.Api.UseCases.SizeModelTypes.Commands.DeactivateSizeModelTypeFactor
{
    public class DeactivateSizeModelTypeFactorController : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPut("/sizemodeltypes/{sizeModelTypeId}/factors/{sizeModelTypeFactorId}/deactivate", async ([FromHeader(Name = "x-requestid")] Guid requestId,
                                                                            [AsParameters] SizeModelTypeService service,
                                                                            [FromBody] DeactivateSizeModelTypeFactorDto DeactivateSizeModelTypeFactorDto) =>
            {
                var request = new DeactivateSizeModelTypeFactorRequest(DeactivateSizeModelTypeFactorDto.SizeModelTypeId, DeactivateSizeModelTypeFactorDto.UserId);

                var result = await service.Mediator.Send(request);

                {
                    Results.BadRequest();
                }

                return Results.Ok(result);

            }).WithTags(Tags.SizeModelTypes);
        }
    }
}
