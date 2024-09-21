
using Microsoft.AspNetCore.Mvc;
using MyPlanner.Plannings.Api.Dtos.SizeModel;

namespace MyPlanner.Plannings.Api.UseCases.SizeModels.Command.ChangeIsStandardSizeModelItem
{
    public class ChangeIsStandarSizeModelItemController : ICarterModule
    {
        void ICarterModule.AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPut("/sizemodels/{sizeModelId}/items/{sizeModelItemId}/changeisstandar", async ([FromHeader(Name = "x-requestid")] Guid requestId,
                                                                                               [AsParameters] SizeModelService service,
                                                                                                string sizeModelId,
                                                                                                string sizeModelItemId,
                                                              [FromBody] ChangeIsStandardSizeModelItemDto changeIsStandardSizeModelItemDto) =>
            {
                var changeIsStandardSizeModelItemRequest = new ChangeIsStandardSizeModelItemRequest(sizeModelItemId, changeIsStandardSizeModelItemDto.IsStandard, changeIsStandardSizeModelItemDto.UserId);

                var result = await service.Mediator.Send(changeIsStandardSizeModelItemRequest);

                return result ? Results.Created() : Results.BadRequest();
            });
        }
    }
}
