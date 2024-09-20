
using Microsoft.AspNetCore.Mvc;
using MyPlanner.Plannings.Api.Dtos.SizeModelType;

namespace MyPlanner.Plannings.Api.UseCases.SizeModelTypes.Commands.ChangeNameSizeModelTypeItem
{
    public class ChangeNameSizeModelTypeItemController : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPut("/sizemodeltypes/{sizeModelTypeId}/items/{sizeModelTypeItemId}/changename", async ([FromHeader(Name = "x-requestid")] Guid requestId,
                                                                              [AsParameters] SizeModelTypeService service,
                                                                              string sizeModelTypeId,
                                                                              string sizeModelTypeItemId,
                                                                              [FromBody] ChangeNameSizeModelTypeItemDto changeNameSizeModelTypeItemDto) =>
            {
                var request = new ChangeNameSizeModelTypeItemRequest(sizeModelTypeItemId, changeNameSizeModelTypeItemDto.Name, changeNameSizeModelTypeItemDto.UserId);

                var result = await service.Mediator.Send(request);

                {
                    Results.BadRequest();
                }

                return Results.Ok(result);

            }).WithTags(Tags.SizeModelTypes);
        }
    }
}

