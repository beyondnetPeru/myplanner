using MyPlanner.Plannings.Api.Dtos.SizeModel;

namespace MyPlanner.Plannings.Api.UseCases.SizeModels.Command.ChangeQuantitySizeModelItem
{
    public class ChangeQuantitySizeModelItemController : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPut("/sizemodels/{sizeModelId}/items/{sizeModelItemId}/changequantity", async ([FromHeader(Name = "x-requestid")] Guid requestId,
                                                                                                [AsParameters] SizeModelService service,
                                                                                                 string sizeModelId,
                                                                                                 string sizeModelItemId,
                                                               [FromBody] ChangeQuantitySizeModelItemDto changeQuantitySizeModelItemDto) =>
            {
                var changeSizeModelTypeItemRequest = new ChangeQuantitySizeModelItemRequest(sizeModelItemId, changeQuantitySizeModelItemDto.Quantity, changeQuantitySizeModelItemDto.UserId);

                var result = await service.Mediator.Send(changeSizeModelTypeItemRequest);

                return result.IsSuccess ? Results.Ok(result) : Results.BadRequest(result);
            });
        }
    }
}
