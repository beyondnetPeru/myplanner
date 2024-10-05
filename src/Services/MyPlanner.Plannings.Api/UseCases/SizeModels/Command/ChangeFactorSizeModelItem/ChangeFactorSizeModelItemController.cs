using MyPlanner.Plannings.Api.Dtos.SizeModel;

namespace MyPlanner.Plannings.Api.UseCases.SizeModels.Command.ChangeFactorSizeModel
{
    public class ChangeFactorSizeModelItemController : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPut("/sizemodels/{sizeModelId}/items/{sizeModelItemId}/changefactor", async ([FromHeader(Name = "x-requestid")] Guid requestId,
                                                                                                [AsParameters] SizeModelService service,
                                                                                                string sizeModelId,
                                                                                                string sizeModelItemId,
                                                               [FromBody] ChangeFactorSizeModelDto changeFactorSizeModelDto) =>
            {
                var changeFactorSizeModelRequest = new ChangeFactorSizeModelItemRequest(sizeModelItemId, changeFactorSizeModelDto.FactorSelected, changeFactorSizeModelDto.UserId);

                var result = await service.Mediator.Send(changeFactorSizeModelRequest);

                return result.IsSuccess ? Results.Ok(result) : Results.BadRequest(result);

            }).WithTags(Tags.SizeModels);
        }
    }
}
