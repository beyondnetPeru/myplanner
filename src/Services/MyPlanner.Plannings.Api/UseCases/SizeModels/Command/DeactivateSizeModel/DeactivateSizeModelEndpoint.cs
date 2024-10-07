using MyPlanner.Plannings.Api.Dtos.SizeModel;

namespace MyPlanner.Plannings.Api.UseCases.SizeModels.Command.DeactivateSizeModel
{
    public class DeactivateSizeModelEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPut("/sizemodels/{sizeModelId}/deactivate", async ([FromHeader(Name = "x-requestid")] Guid requestId, [AsParameters] SizeModelService service,
                                                                       string sizeModelId,
                                                                       DeactivateSizeModelDto activateSizeModelDto) =>
            {
                var request = new DeactivateSizeModelCommand(sizeModelId, activateSizeModelDto.UserId);

                var result = await service.Mediator.Send(request);

                return result.IsSuccess ? Results.Ok(result) : Results.BadRequest(result);

            }).WithTags(Tags.SizeModels);

        }
    }
}
