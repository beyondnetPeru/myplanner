using MyPlanner.Plannings.Api.Dtos.SizeModelType;
using MyPlanner.Plannings.Api.UseCases.SizeModelTypes.Commands.ChangeCodeSizeModelTypeItem;

namespace MyPlanner.Plannings.Api.UseCases.SizeModelTypes.Commands.ChangeNameSizeModelType
{
    public class ChangeNameSizeModelTypeEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPut("/sizemodeltypes/{sizeModelTypeId}/changename", async ([FromHeader(Name = "x-requestid")] Guid requestId,
                                                                              [AsParameters] SizeModelTypeService service,
                                                                              string sizeModelTypeId,
                                                                              [FromBody] ChangeNameSizeModelTypeDto changeNameSizeModelTypeDto) =>
            {
                var request = new ChangeCodeSizeModelTypeItemCommand(sizeModelTypeId, changeNameSizeModelTypeDto.Name, changeNameSizeModelTypeDto.UserId);

                var result = await service.Mediator.Send(request);

                return result.IsSuccess ? Results.Ok(result) : Results.BadRequest(result);

            }).WithTags(Tags.SizeModelTypes);
        }

    }
}
