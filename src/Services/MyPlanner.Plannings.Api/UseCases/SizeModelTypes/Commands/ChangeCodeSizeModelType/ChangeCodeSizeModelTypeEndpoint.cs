

namespace MyPlanner.Plannings.Api.UseCases.SizeModelTypes.Commands.ChangeCodeSizeModelType
{
    public class ChangeCodeSizeModelTypeEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPut("/sizemodeltypes/{sizeModelTypeId}/changecode", async ([FromHeader(Name = "x-requestid")] Guid requestId,
                                                                            [AsParameters] SizeModelTypeService service,
                                                                            string sizeModelTypeId,
                                                                            [FromBody] ChangeCodeSizeModelTypeDto changeCodeSizeModelTypeDto) =>
            {
                var request = new ChangeCodeSizeModelTypeCommand(sizeModelTypeId, changeCodeSizeModelTypeDto.Code, changeCodeSizeModelTypeDto.UserId);

                var result = await service.Mediator.Send(request);

                return result.IsSuccess ? Results.Ok(result) : Results.BadRequest(result);

            }).WithTags(Tags.SizeModelTypes);

        }
    }
}
