using MyPlanner.Plannings.Api.Dtos.SizeModelType;

namespace MyPlanner.Plannings.Api.UseCases.SizeModelTypes.Commands.ActivateSizeModelType
{
    public class ActivateSizeModelTypeEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPut("/sizemodeltypes/{sizeModelTypeId}/activate", async ([FromHeader(Name = "x-requestid")] Guid requestId,
                                                                             [AsParameters] SizeModelTypeService service,
                                                                             string sizeModelTypeId,
                                                                             [FromBody] ActivateSizeModelTypeDto activateSizeModelTypeDto) =>
            {
                var request = new ActivateSizeModelTypeCommand(sizeModelTypeId, activateSizeModelTypeDto.UserId);

                var result = await service.Mediator.Send(request);

                return result.IsSuccess ? Results.Ok(result) : Results.BadRequest(result);

            }).WithTags(Tags.SizeModelTypes);
        }

    }
}
