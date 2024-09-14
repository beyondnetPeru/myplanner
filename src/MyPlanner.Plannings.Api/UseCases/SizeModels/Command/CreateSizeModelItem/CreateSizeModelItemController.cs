using Microsoft.AspNetCore.Mvc;
using MyPlanner.Plannings.Api.Dtos.SizeModel;

namespace MyPlanner.Plannings.Api.UseCases.SizeModels.Command.CreateSizeModelItem
{
    public class CreateSizeModelItemController : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/sizemodels/{sizeModelId}/items", async ([FromHeader(Name = "x-requestid")] Guid requestId, [AsParameters] SizeModelService service, [FromBody] CreateSizeModelItemDto createSizeModelItemDto) =>
            {
                var createSizeModelItemRequest = new CreateSizeModelItemRequest(createSizeModelItemDto.SizeModelId,
                                                                                createSizeModelItemDto.ProfileName,
                                                                                createSizeModelItemDto.ProfileAvgRateAmount,
                                                                                createSizeModelItemDto.SizeModelFactorTypeId,
                                                                                createSizeModelItemDto.ProfileValueSelected,
                                                                                createSizeModelItemDto.ProfileCountValue,
                                                                                createSizeModelItemDto.TotalCost,
                                                                                createSizeModelItemDto.UserId);

                var result = await service.Mediator.Send(createSizeModelItemRequest);

                return result ? Results.Created() : Results.BadRequest();

            }).WithTags(Tags.SizeModels);

        }
    }
}
