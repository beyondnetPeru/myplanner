using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using MyPlanner.Plannings.Api.Dtos.SizeModel;

namespace MyPlanner.Plannings.Api.UseCases.SizeModels.CreateSizeModel
{
    public class CreateSizeModelController(IMediator mediator) : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/sizemodels/", async ([FromHeader(Name = "x-requestid")] Guid requestId, [AsParameters] SizeModelService service, [FromBody] CreateSizeModelDto createSizeModelDto) =>
            {
                var createSizeModelRequest = new
                                CreateSizeModelRequest(createSizeModelDto.SizeModelTypeCode,
                                                       createSizeModelDto.Name,
                                                       createSizeModelDto.Description,
                                                       createSizeModelDto.IsStandard,
                                                       createSizeModelDto.UserId,
                                                       createSizeModelDto.SizeModelItems);

                var result = await mediator.Send(createSizeModelRequest);

                return result ? Results.Created() : Results.BadRequest();

            }).WithTags(Tags.SizeModels);
        }
    }
}
