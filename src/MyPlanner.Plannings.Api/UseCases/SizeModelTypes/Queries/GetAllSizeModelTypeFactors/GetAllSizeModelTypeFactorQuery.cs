using MyPlanner.Plannings.Api.Dtos.SizeModelType;

namespace MyPlanner.Plannings.Api.UseCases.SizeModelTypes.Queries.GetAllSizeModelTypeFactors
{
    public class GetAllSizeModelTypeFactorQuery : IRequest<IEnumerable<SizeModelTypeFactorDto>>
    {
        public GetAllSizeModelTypeFactorQuery(string sizeModelTypeId)
        {
            SizeModelTypeId = sizeModelTypeId;
        }

        public string SizeModelTypeId { get; }
    }
}
