using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using MyPlanner.Plannings.Api.Dtos.SizeModel;

namespace MyPlanner.Plannings.Api.UseCases.SizeModels.ActivateSizeModel
{
    public class ActivateSizeModelController : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPut("/sisemodels/{sizeModelId}/activate", async ([FromHeader(Name = "x-requestid")] Guid requestId, [AsParameters] SizeModelService service, ActivateSizeModelDto activateSizeModelDto) =>
            {
                var request = new ActivateSizeModelRequest(activateSizeModelDto.SizeModelId, activateSizeModelDto.UserId);

                var result = await service.Mediator.Send(request);

                if (!result)
                {
                    Results.BadRequest();
                }

                return Results.Ok(result);

            }).WithTags(Tags.SizeModels);

        }
    }
}
