

namespace MyPlanner.Plannings.Api.UseCases.SizeModels.Command.ChangeSizeModelName
{
    public class ChangeNameSizeModelEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPut("/sizemodels/{sizeModelId}/changename", async ([FromHeader(Name = "x-requestid")] Guid requestId,
                                                                [AsParameters] SizeModelService service,
                                                                string sizeModelId,
                                                                [FromBody] ChangeNameSizeModelDto changeNameSizeModelDto) =>
            {
                var request = new ChangeNameSizeModelCommand(sizeModelId, changeNameSizeModelDto.Name, changeNameSizeModelDto.UserId);

                var result = await service.Mediator.Send(request);

                return result.IsSuccess ? Results.Ok(result) : Results.BadRequest(result);
            }).WithTags(Tags.SizeModels);
        }
    }
}
