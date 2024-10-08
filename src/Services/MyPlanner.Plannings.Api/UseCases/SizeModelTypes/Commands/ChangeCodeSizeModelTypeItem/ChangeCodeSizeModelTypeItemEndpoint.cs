

namespace MyPlanner.Plannings.Api.UseCases.SizeModelTypes.Commands.ChangeCodeSizeModelTypeItem
{
    public class ChangeCodeSizeModelTypeItemEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPut("/sizemodeltypes/{sizeModelTypeId}/items/{sizeModelTypeItemId}/changecode", async ([FromHeader(Name = "x-requestid")] Guid requestId,
                                                                              [AsParameters] SizeModelTypeService service,
                                                                              string sizeModelTypeId,
                                                                              string sizeModelTypeItemId,
                                                                              [FromBody] ChangeCodeSizeModelTypeItemDto changeCodeSizeModelTypeItemDto) =>
            {
                var request = new ChangeCodeSizeModelTypeItemCommand(sizeModelTypeItemId, changeCodeSizeModelTypeItemDto.Code, changeCodeSizeModelTypeItemDto.UserId);

                var result = await service.Mediator.Send(request);

                return result.IsSuccess ? Results.Ok(result) : Results.BadRequest(result);

            }).WithTags(Tags.SizeModelTypes);
        }
    }
}
