using MyPlanner.Plannings.Api.Dtos.SizeModelType;

namespace MyPlanner.Plannings.Api.UseCases.SizeModelTypes.Commands.DeactivateSizeModelType
{
    public class DeactivateSizeModelTypeEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPut("/sizemodeltypes/{sizeModelTypeId}/deactivate", async ([FromHeader(Name = "x-requestid")] Guid requestId,
                                                                            [AsParameters] SizeModelTypeService service,
                                                                            string sizeModelTypeId,
                                                                            [FromBody] DeactivateSizeModelTypeDto deactivateSizeModelTypeDto) =>
            {
                var request = new DeactivateSizeModelTypeCommand(sizeModelTypeId, deactivateSizeModelTypeDto.UserId);

                var result = await service.Mediator.Send(request);

                return result.IsSuccess ? Results.Ok(result) : Results.BadRequest(result);

            }).WithTags(Tags.SizeModelTypes);
        }
    }
}
