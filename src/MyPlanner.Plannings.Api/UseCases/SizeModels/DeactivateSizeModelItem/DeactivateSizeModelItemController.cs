using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using MyPlanner.Plannings.Api.Dtos.SizeModel;

namespace MyPlanner.Plannings.Api.UseCases.SizeModels.DeactivateSizeModelItem
{
    public class DeactivateSizeModelItemController : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPut("/sizemodels/{sizeModelId}/deactivate/{sizeModelItemId}", async ([FromHeader(Name = "x-requestid")] Guid requestId, [AsParameters] SizeModelService service, DeactivateSizeModelItemDto deactivateSizeModelItemDto) =>
            {
                var request = new DeactivateSizeModelItemRequest(deactivateSizeModelItemDto.SizeModelId, deactivateSizeModelItemDto.SizeModelItemId, deactivateSizeModelItemDto.UserId);

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
