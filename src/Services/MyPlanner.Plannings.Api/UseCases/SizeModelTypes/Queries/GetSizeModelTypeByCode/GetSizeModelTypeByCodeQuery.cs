using MyPlanner.Plannings.Api.Dtos.SizeModelType;

namespace MyPlanner.Plannings.Api.UseCases.SizeModelTypes.Queries.GetSizeModelTypeByCode
{
    public class GetSizeModelTypeByCodeQuery : IRequest<SizeModelTypeDto>
    {
        public GetSizeModelTypeByCodeQuery(string code)
        {
            Code = code;
        }

        public string Code { get; }
    }
}
