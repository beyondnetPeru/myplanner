﻿
using Microsoft.AspNetCore.Mvc;
using MyPlanner.Plannings.Api.Dtos.SizeModelType;

namespace MyPlanner.Plannings.Api.UseCases.SizeModelTypes.Commands.DeleteSizeModelTypeItem
{
    public class DeleteSizeModelTypeItemController : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapDelete("/sizemodeltypes/{sizeModelTypeId}/items/{sizeModelTypeItemId}", async ([FromHeader(Name = "x-requestid")] Guid requestId,
                                                                             [AsParameters] SizeModelTypeService service,
                                                                             string sizeModelTypeItemId,
                                                                             [FromBody] DeleteSizeModelTypeItemDto deleteSizeModelTypeItemDto) =>
            {
                var request = new DeleteSizeModelTypeItemRequest(sizeModelTypeItemId, deleteSizeModelTypeItemDto.UserId);

                var result = await service.Mediator.Send(request);

                {
                    Results.BadRequest();
                }

                return Results.Ok(result);

            }).WithTags(Tags.SizeModelTypes);
        }
    }
}