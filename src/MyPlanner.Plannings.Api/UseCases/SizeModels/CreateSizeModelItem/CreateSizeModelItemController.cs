using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using MyPlanner.Plannings.Api.Dtos.SizeModel;

namespace MyPlanner.Plannings.Api.UseCases.SizeModels.CreateSizeModelItem
{
    public class CreateSizeModelItemController(IMediator mediator) : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/sizemodels/{sizeModelId}/items", async ([FromHeader(Name = "x-requestid")] Guid requestId, [FromBody] CreateSizeModelItemDto createSizeModelItemDto) =>
            {
                var createSizeModelItemRequest = new CreateSizeModelItemRequest(createSizeModelItemDto.SizeModelId,
                                                                                createSizeModelItemDto.ProfileName,
                                                                                createSizeModelItemDto.ProfileAvgRateAmount,
                                                                                createSizeModelItemDto.SizeModelFactorTypeCode,
                                                                                createSizeModelItemDto.ProfileValueSelected,
                                                                                createSizeModelItemDto.ProfileCountValue,
                                                                                createSizeModelItemDto.TotalCost,
                                                                                createSizeModelItemDto.UserId);

                var result = await mediator.Send(createSizeModelItemRequest);

                return result ? Results.Created() : Results.BadRequest();

            }).WithTags(Tags.SizeModels);

        }
    }
}
