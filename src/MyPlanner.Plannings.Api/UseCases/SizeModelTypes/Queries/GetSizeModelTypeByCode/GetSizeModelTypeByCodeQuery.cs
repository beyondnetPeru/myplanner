using MediatR;
using MyPlanner.Plannings.Api.Dtos.SizeModel;

namespace MyPlanner.Plannings.Api.UseCases.SizeModelTypes.Queries.GetSizeModelTypeByCode
{
    public class GetSizeModelTypeByCodeQuery : IRequest<SizeModelDto>
    {
        public GetSizeModelTypeByCodeQuery(string code)
        {
            Code = code;
        }

        public string Code { get; }
    }
}
