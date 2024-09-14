using Microsoft.AspNetCore.Mvc;
using MyPlanner.Plannings.Api.Dtos.SizeModelType;

namespace MyPlanner.Plannings.Api.UseCases.SizeModelTypes.ChangeNameSizeModelType
{
    public class ChangeNameSizeModelTypeController(IMediator mediator) : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPut("/sizemodeltypes/{sizeModelTypeId}/changename", async ([FromHeader(Name = "x-requestid")] Guid requestId,
                                                                            [AsParameters] SizeModelTypeService service,
                                                                           [FromBody] ChangeNameSizeModelTypeDto changeNameSizeModelTypeDto) =>
            {
                var request = new ChangeNameSizeModelTypeRequest(changeNameSizeModelTypeDto.SizeModelTypeId, changeNameSizeModelTypeDto.Name, changeNameSizeModelTypeDto.UserId);

                var result = await mediator.Send(request);

                {
                    Results.BadRequest();
                }

                return Results.Ok(result);

            }).WithTags(Tags.SizeModelTypes);
        }

    }
}
