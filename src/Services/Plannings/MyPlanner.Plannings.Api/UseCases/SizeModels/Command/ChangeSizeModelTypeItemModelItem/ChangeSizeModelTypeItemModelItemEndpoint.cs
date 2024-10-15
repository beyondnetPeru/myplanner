

namespace MyPlanner.Plannings.Api.UseCases.SizeModels.Command.ChangeSizeModelTypeItem
{
    public class ChangeSizeModelTypeItemModelItemEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPut("/sizemodels/{sizeModelId}/items/{sizeModelItemId}/changesyzemodeltypeitem", async ([FromHeader(Name = "x-requestid")] Guid requestId,
                                                                                                [AsParameters] SizeModelService service,
                                                                                                 string sizeModelId,
                                                                                                 string sizeModelItemId,
                                                               [FromBody] ChangeSizeModelTypeItemDto changeSizeModelTypeItemDto) =>
            {
                var changeSizeModelTypeItemRequest = new ChangeSizeModelTypeItemModelItemCommand(sizeModelItemId,
                                                                                                 changeSizeModelTypeItemDto.SizeModelTypeItemId,
                                                                                                 changeSizeModelTypeItemDto.SizeModelItemTypeCode,
                                                                                                 changeSizeModelTypeItemDto.UserId);


                var result = await service.Mediator.Send(changeSizeModelTypeItemRequest);

                return result.IsSuccess ? Results.Ok(result) : Results.BadRequest(result);
            });

        }
    }
}
