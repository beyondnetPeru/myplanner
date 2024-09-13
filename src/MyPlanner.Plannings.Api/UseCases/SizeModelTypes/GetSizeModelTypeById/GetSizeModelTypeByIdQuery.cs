using MediatR;
using MyPlanner.Plannings.Api.Dtos.SizeModel;

namespace MyPlanner.Plannings.Api.UseCases.SizeModelTypes.GetSizeModelTypeById
{
    public class GetSizeModelTypeByIdQuery : IRequest<SizeModelDto>
    {
        public GetSizeModelTypeByIdQuery(string id)
        {
            Id = id;
        }

        public string Id { get; }
    }
}
