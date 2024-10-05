﻿using MyPlanner.Plannings.Api.Dtos.SizeModel;

namespace MyPlanner.Plannings.Api.UseCases.SizeModels.Command.ChangeSizeModelName
{
    public class ChangeNameSizeModelController : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPut("/sizemodels/{sizeModelId}/changename", async ([FromHeader(Name = "x-requestid")] Guid requestId,
                                                                [AsParameters] SizeModelService service,
                                                                string sizeModelId,
                                                                [FromBody] ChangeNameSizeModelDto changeNameSizeModelDto) =>
            {
                var request = new ChangeNameSizeModelRequest(sizeModelId, changeNameSizeModelDto.Name, changeNameSizeModelDto.UserId);

                var result = await service.Mediator.Send(request);

                return result.IsSuccess ? Results.Ok(result) : Results.BadRequest(result);
            }).WithTags(Tags.SizeModels);
        }
    }
}
