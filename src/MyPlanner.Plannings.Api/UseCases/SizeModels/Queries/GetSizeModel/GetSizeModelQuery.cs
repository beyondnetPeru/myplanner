using MediatR;
using MyPlanner.Plannings.Api.Dtos.SizeModel;

namespace MyPlanner.Plannings.Api.UseCases.SizeModels.Queries.GetSizeModel
{
    public class GetSizeModelQuery : IRequest<SizeModelDto>
    {
        public string SizeModelId { get; set; }

        public GetSizeModelQuery(string sizeModelId)
        {
            SizeModelId = sizeModelId;
        }
    }
}
