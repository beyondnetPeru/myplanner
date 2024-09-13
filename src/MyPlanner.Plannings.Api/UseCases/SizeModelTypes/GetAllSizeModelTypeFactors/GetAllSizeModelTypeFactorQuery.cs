using MediatR;
using MyPlanner.Plannings.Api.Dtos.SizeModelType;

namespace MyPlanner.Plannings.Api.UseCases.SizeModelTypes.GetAllSizeModelTypeFactors
{
    public class GetAllSizeModelTypeFactorQuery : IRequest<IEnumerable<SizeModelTypeFactorDto>>
    {
        public GetAllSizeModelTypeFactorQuery(string sizeModelType)
        {
            SizeModelType = sizeModelType;
        }

        public string SizeModelType { get; }
    }
}
