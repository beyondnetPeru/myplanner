

namespace MyPlanner.Plannings.Api.UseCases.SizeModels.Command.DeactivateSizeModelItem
{
    public class DeactivateSizeModelItemEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPut("/sizemodels/{sizeModelId}/items/{sizeModelItemId}/deactivate", async ([FromHeader(Name = "x-requestid")] Guid requestId,
                                                                                              [AsParameters] SizeModelService service,
                                                                                              string sizeModelId,
                                                                                              string sizeModelItemId,
                                                                                              DeactivateSizeModelItemDto deactivateSizeModelItemDto) =>
            {
                var request = new DeactivateSizeModelItemCommand(sizeModelId, sizeModelItemId, deactivateSizeModelItemDto.UserId);

                var result = await service.Mediator.Send(request);

                return result.IsSuccess ? Results.Ok(result) : Results.BadRequest(result);

            }).WithTags(Tags.SizeModels);
        }
    }
}
