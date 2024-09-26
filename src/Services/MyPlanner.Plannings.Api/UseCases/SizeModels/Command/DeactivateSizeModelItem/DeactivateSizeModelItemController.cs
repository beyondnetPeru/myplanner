using Microsoft.AspNetCore.Mvc;
using MyPlanner.Plannings.Api.Dtos.SizeModel;

namespace MyPlanner.Plannings.Api.UseCases.SizeModels.Command.DeactivateSizeModelItem
{
    public class DeactivateSizeModelItemController : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPut("/sizemodels/{sizeModelId}/items/{sizeModelItemId}/deactivate", async ([FromHeader(Name = "x-requestid")] Guid requestId,
                                                                                              [AsParameters] SizeModelService service,
                                                                                              string sizeModelId,
                                                                                              string sizeModelItemId,
                                                                                              DeactivateSizeModelItemDto deactivateSizeModelItemDto) =>
            {
                var request = new DeactivateSizeModelItemRequest(sizeModelId, sizeModelItemId, deactivateSizeModelItemDto.UserId);

                var result = await service.Mediator.Send(request);

                if (!result)
                {
                    Results.BadRequest();
                }

                return Results.Ok(result);

            }).WithTags(Tags.SizeModels);
        }
    }
}
