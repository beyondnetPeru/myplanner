
using Microsoft.AspNetCore.Mvc;
using MyPlanner.Plannings.Api.Dtos.SizeModel;

namespace MyPlanner.Plannings.Api.UseCases.SizeModels.Command.ChangeTotalCostSizeModelItem
{
    public class ChangeTotalCostSizeModelItemController : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPut("/sizemodels/{sizeModelId}/items/{sizeModelItemId}/changetotalcost", async ([FromHeader(Name = "x-requestid")] Guid requestId,
                                                                                              [AsParameters] SizeModelService service,
                                                                                               string sizeModelId,
                                                                                               string sizeModelItemId,
                                                             [FromBody] ChangeTotalCostSizeModelItemDto changeTotalCostSizeModelItemDto) =>
            {
                var changeTotalCostSizeModelItemRequest = new ChangeTotalCostSizeModelItemRequest(sizeModelItemId, changeTotalCostSizeModelItemDto.TotalCost, changeTotalCostSizeModelItemDto.UserId);

                var result = await service.Mediator.Send(changeTotalCostSizeModelItemRequest);

                return result ? Results.Created() : Results.BadRequest();
            });
        }
    }
}
