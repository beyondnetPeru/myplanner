

namespace MyPlanner.Plannings.Api.UseCases.SizeModelTypes.Commands.DeleteSizeModelType
{
    public class DeleteSizeModelTypeEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapDelete("/sizemodeltypes/{sizeModelTypeId}", async ([FromHeader(Name = "x-requestid")] Guid requestId,
                                                                             [AsParameters] SizeModelTypeService service,
                                                                             string sizeModelTypeId,
                                                                             [FromBody] DeleteSizeModelTypeDto deleteSizeModelTypeDto) =>
            {
                var request = new DeleteSizeModelTypeCommand(sizeModelTypeId, deleteSizeModelTypeDto.UserId);

                var result = await service.Mediator.Send(request);

                return result.IsSuccess ? Results.Ok(result) : Results.BadRequest(result);

            }).WithTags(Tags.SizeModelTypes);
        }
    }
}
