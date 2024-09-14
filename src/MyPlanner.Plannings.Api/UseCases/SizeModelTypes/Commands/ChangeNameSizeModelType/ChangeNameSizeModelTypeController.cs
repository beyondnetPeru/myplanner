using Microsoft.AspNetCore.Mvc;
using MyPlanner.Plannings.Api.Dtos.SizeModelType;

namespace MyPlanner.Plannings.Api.UseCases.SizeModelTypes.Commands.ChangeNameSizeModelType
{
    public class ChangeNameSizeModelTypeController : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPut("/sizemodeltypes/{sizeModelTypeId}/changename", async ([FromHeader(Name = "x-requestid")] Guid requestId,
                                                                              [AsParameters] SizeModelTypeService service,
                                                                              [FromBody] ChangeNameSizeModelTypeDto changeNameSizeModelTypeDto) =>
            {
                var request = service.Mapper.Map<ChangeNameSizeModelTypeRequest>(changeNameSizeModelTypeDto);

                var result = await service.Mediator.Send(request);

                {
                    Results.BadRequest();
                }

                return Results.Ok(result);

            }).WithTags(Tags.SizeModelTypes);
        }

    }
}
