

namespace MyPlanner.Plannings.Api.UseCases.SizeModelTypes.Commands.ActivateSizeModelTypeFactor
{
    public class ActivateSizeModelTypeItemEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPut("/sizemodeltypes/{sizeModelTypeId}/items/{sizeModelTypeItemId}/activate", async ([FromHeader(Name = "x-requestid")] Guid requestId,
                                                                             [AsParameters] SizeModelTypeService service,
                                                                             string sizeModelTypeId,
                                                                             string sizeModelTypeItemId,
                                                                             [FromBody] ActivateSizeModelTypeItemDto activateSizeModelTypeItemDto) =>
            {
                var request = new ActivateSizeModelTypeItemCommand(sizeModelTypeItemId, activateSizeModelTypeItemDto.UserId);

                var result = await service.Mediator.Send(request);

                return result.IsSuccess ? Results.Ok(result) : Results.BadRequest(result);

            }).WithTags(Tags.SizeModelTypes);
        }
    }
}
