using MyPlanner.Plannings.Api.Dtos.SizeModel;

namespace MyPlanner.Plannings.Api.UseCases.SizeModels.Command.ActivateSizeModelItem
{
    public class ActivateSizeModelItemController : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPut("/sizemodels/{sizeModelId}/items/{sizeModelItemId}/activate",
                async ([FromHeader(Name = "x-requestid")] Guid requestId, [FromBody] ActivateSizeModelItemDto activateSizeModelItemDto,
                                                          string sizeModelId, string sizeModelItemId,
                                                          [AsParameters] SizeModelService service) =>
            {
                var request = new ActivateSizeModelItemRequest(sizeModelId, sizeModelItemId, activateSizeModelItemDto.UserId);

                var result = await service.Mediator.Send(request);

                return result.IsSuccess ? Results.Ok(result) : Results.BadRequest(result);

            }).WithTags(Tags.SizeModels);
        }
    }
}
