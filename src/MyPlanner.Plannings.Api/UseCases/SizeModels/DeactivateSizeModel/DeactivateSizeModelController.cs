﻿using Microsoft.AspNetCore.Mvc;
using MyPlanner.Plannings.Api.Dtos.SizeModel;

namespace MyPlanner.Plannings.Api.UseCases.SizeModels.DeactivateSizeModel
{
    public class DeactivateSizeModelController : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPut("/sizemodels/{sizeModelId}/deactivate", async ([FromHeader(Name = "x-requestid")] Guid requestId, [AsParameters] SizeModelService service, DeactivateSizeModelDto activateSizeModelDto) =>
            {
                var request = new DeactivateSizeModelRequest(activateSizeModelDto.SizeModelId, activateSizeModelDto.UserId);

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
