using Microsoft.AspNetCore.Mvc;
using MyPlanner.Plannings.Api.Dtos.SizeModelType;

namespace MyPlanner.Plannings.Api.UseCases.SizeModelTypes.ChangeCodeSizeModelType
{
    public class ChangeCodeSizeModelTypeController(IMediator mediator) : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPut("/sizemodeltypes/{sizeModelTypeId}/changecode", async ([FromHeader(Name = "x-requestid")] Guid requestId,
                                                                            [AsParameters] SizeModelTypeService service,
                                                                            [FromBody] ChangeCodeSizeModelTypeDto changeCodeSizeModelTypeDto) =>
            {
                var request = new ChangeCodeSizeModelTypeRequest(changeCodeSizeModelTypeDto.SizeModelTypeId, changeCodeSizeModelTypeDto.Code, changeCodeSizeModelTypeDto.UserId);

                var result = await mediator.Send(request);

                {
                    Results.BadRequest();
                }

                return Results.Ok(result);



            }).WithTags(Tags.SizeModelTypes);

        }
    }
}
