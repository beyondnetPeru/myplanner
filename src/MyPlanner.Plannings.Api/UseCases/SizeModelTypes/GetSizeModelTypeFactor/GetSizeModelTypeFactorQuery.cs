using MediatR;
using MyPlanner.Plannings.Api.Dtos.SizeModelType;

namespace MyPlanner.Plannings.Api.UseCases.SizeModelTypes.GetSizeModelTypeFactor
{
    public class GetSizeModelTypeFactorQuery : IRequest<SizeModelTypeFactorDto>
    {
        public GetSizeModelTypeFactorQuery(string sizeModelTypeId, string sizeModelTypeFactorId)
        {
            SizeModelTypeId = sizeModelTypeId;
            SizeModelTypeFactorId = sizeModelTypeFactorId;
        }

        public string SizeModelTypeId { get; }
        public string SizeModelTypeFactorId { get; }
    }
}
