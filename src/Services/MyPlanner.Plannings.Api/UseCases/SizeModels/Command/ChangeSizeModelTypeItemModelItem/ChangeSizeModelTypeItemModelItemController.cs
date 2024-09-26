using Microsoft.AspNetCore.Mvc;
using MyPlanner.Plannings.Api.Dtos.SizeModel;

namespace MyPlanner.Plannings.Api.UseCases.SizeModels.Command.ChangeSizeModelTypeItem
{
    public class ChangeSizeModelTypeItemModelItemController : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPut("/sizemodels/{sizeModelId}/items/{sizeModelItemId}/changesyzemodeltypeitem", async ([FromHeader(Name = "x-requestid")] Guid requestId,
                                                                                                [AsParameters] SizeModelService service,
                                                                                                 string sizeModelId,
                                                                                                 string sizeModelItemId,
                                                               [FromBody] ChangeSizeModelTypeItemDto changeSizeModelTypeItemDto) =>
            {
                var changeSizeModelTypeItemRequest = new ChangeSizeModelTypeItemModelItemRequest(sizeModelItemId,
                                                                                                 changeSizeModelTypeItemDto.SizeModelTypeItemId,
                                                                                                 changeSizeModelTypeItemDto.SizeModelItemTypeCode,
                                                                                                 changeSizeModelTypeItemDto.UserId);


                var result = await service.Mediator.Send(changeSizeModelTypeItemRequest);

                return result ? Results.Created() : Results.BadRequest();
            });

        }
    }
}
