

namespace MyPlanner.Plannings.Api.UseCases.SizeModels.Command.CreateSizeModel
{
    public class CreateSizeModelEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/sizemodels/", async ([FromHeader(Name = "x-requestid")] Guid requestId, [AsParameters] SizeModelService service, [FromBody] CreateSizeModelDto createSizeModelDto) =>
            {
                var createSizeModelRequest = service.Mapper.Map<CreateSizeModelCommand>(createSizeModelDto);

                var result = await service.Mediator.Send(createSizeModelRequest);

                return result.IsSuccess ? Results.Ok(result) : Results.BadRequest(result);

            }).WithTags(Tags.SizeModels);
        }
    }
}
