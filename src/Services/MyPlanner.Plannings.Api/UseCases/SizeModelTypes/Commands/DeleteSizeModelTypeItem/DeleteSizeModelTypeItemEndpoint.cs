

namespace MyPlanner.Plannings.Api.UseCases.SizeModelTypes.Commands.DeleteSizeModelTypeItem
{
    public class DeleteSizeModelTypeItemEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapDelete("/sizemodeltypes/{sizeModelTypeId}/items/{sizeModelTypeItemId}", async ([FromHeader(Name = "x-requestid")] Guid requestId,
                                                                             [AsParameters] SizeModelTypeService service,
                                                                             string sizeModelTypeItemId,
                                                                             [FromBody] DeleteSizeModelTypeItemDto deleteSizeModelTypeItemDto) =>
            {
                var request = new DeleteSizeModelTypeItemCommand(sizeModelTypeItemId, deleteSizeModelTypeItemDto.UserId);

                var result = await service.Mediator.Send(request);

                return result.IsSuccess ? Results.Ok(result) : Results.BadRequest(result);

            }).WithTags(Tags.SizeModelTypes);
        }
    }
}
