using MyPlanner.Plannings.Api.Dtos.SizeModel;

namespace MyPlanner.Plannings.Api.UseCases.SizeModels.Command.ChangeIsStandardSizeModelItem
{
    public class ChangeIsStandarSizeModelItemEndpoint : ICarterModule
    {
        void ICarterModule.AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPut("/sizemodels/{sizeModelId}/items/{sizeModelItemId}/changeisstandar", async ([FromHeader(Name = "x-requestid")] Guid requestId,
                                                                                                [AsParameters] SizeModelService service,
                                                                                                string sizeModelId,
                                                                                                string sizeModelItemId,
                                                                                                [FromBody] ChangeIsStandardSizeModelItemDto changeIsStandardSizeModelItemDto) =>
            {
                var changeIsStandardSizeModelItemRequest = new ChangeIsStandardSizeModelItemCommand(sizeModelItemId, changeIsStandardSizeModelItemDto.IsStandard, changeIsStandardSizeModelItemDto.UserId);

                var result = await service.Mediator.Send(changeIsStandardSizeModelItemRequest);

                return result.IsSuccess ? Results.Ok(result) : Results.BadRequest(result);
            });
        }
    }
}
