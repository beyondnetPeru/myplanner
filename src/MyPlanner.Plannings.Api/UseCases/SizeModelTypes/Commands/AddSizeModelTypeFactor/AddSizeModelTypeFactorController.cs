using Microsoft.AspNetCore.Mvc;
using MyPlanner.Plannings.Api.Dtos.SizeModelType;

namespace MyPlanner.Plannings.Api.UseCases.SizeModelTypes.Commands.AddSizeModelTypeFactor
{
    public class AddSizeModelTypeFactorController : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/sizemodeltypes/{sizeModelTypeId}/factors", async ([FromHeader(Name = "x-requestid")] Guid requestId,
                                                                            [AsParameters] SizeModelTypeService service,
                                                                            [FromBody] AddSizeModelTypeFactorDto addSizeModelTypeFactorDto) =>
            {
                var request = new AddSizeModelTypeFactorRequest(addSizeModelTypeFactorDto.SizeModelId, addSizeModelTypeFactorDto.Code, addSizeModelTypeFactorDto.Name, addSizeModelTypeFactorDto.UserId);

                var result = await service.Mediator.Send(request);

                {
                    Results.BadRequest();
                }

                return Results.Ok(result);

            }).WithTags(Tags.SizeModelTypes);
        }
    }
}
