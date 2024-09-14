﻿using Microsoft.AspNetCore.Mvc;
using MyPlanner.Plannings.Api.Dtos.SizeModel;

namespace MyPlanner.Plannings.Api.UseCases.SizeModels.Command.ActivateSizeModelItem
{
    public class ActivateSizeModelItemController : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPut("/sisemodels/{sizeModelId}/items/{sizeModelItemId}/activate", async ([FromHeader(Name = "x-requestid")] Guid requestId, [FromBody] ActivateSizeModelItemDto activateSizeModelItemDto, [AsParameters] SizeModelService service) =>
            {
                var request = new ActivateSizeModelItemRequest(activateSizeModelItemDto.SizeModelId, activateSizeModelItemDto.SizeModelItemId, activateSizeModelItemDto.UserId);

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