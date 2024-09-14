using Microsoft.AspNetCore.Mvc;
using MyPlanner.Plannings.Api.Dtos.SizeModelType;

namespace MyPlanner.Plannings.Api.UseCases.SizeModelTypes.Commands.ChangeCodeSizeModelType
{
    public class ChangeCodeSizeModelTypeController : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPut("/sizemodeltypes/{sizeModelTypeId}/changecode", async ([FromHeader(Name = "x-requestid")] Guid requestId,
                                                                            [AsParameters] SizeModelTypeService service,
                                                                            [FromBody] ChangeCodeSizeModelTypeDto changeCodeSizeModelTypeDto) =>
            {
                var request = service.Mapper.Map<ChangeCodeSizeModelTypeRequest>(changeCodeSizeModelTypeDto);

                var result = await service.Mediator.Send(request);

                {
                    Results.BadRequest();
                }

                return Results.Ok(result);



            }).WithTags(Tags.SizeModelTypes);

        }
    }
}
