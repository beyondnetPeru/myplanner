

namespace MyPlanner.Plannings.Api.UseCases.SizeModels.Command.ActivateSizeModel
{
    public class ActivateSizeModelEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPut("/sizemodels/{sizeModelId}/activate", async ([FromHeader(Name = "x-requestid")] Guid requestId, [AsParameters] SizeModelService service,
                                                                    string sizeModelId, ActivateSizeModelDto activateSizeModelDto) =>
            {
                var request = new ActivateSizeModelCommand(sizeModelId, activateSizeModelDto.UserId);

                var result = await service.Mediator.Send(request);

                return result.IsSuccess ? Results.Ok(result) : Results.BadRequest(result);

            }).WithTags(Tags.SizeModels);

        }
    }
}
