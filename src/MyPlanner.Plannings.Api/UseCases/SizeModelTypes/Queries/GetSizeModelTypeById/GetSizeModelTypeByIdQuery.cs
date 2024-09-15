using MyPlanner.Plannings.Api.Dtos.SizeModelType;

namespace MyPlanner.Plannings.Api.UseCases.SizeModelTypes.Queries.GetSizeModelTypeById
{
    public class GetSizeModelTypeByIdQuery : IRequest<SizeModelTypeDto>
    {
        public GetSizeModelTypeByIdQuery(string id)
        {
            Id = id;
        }

        public string Id { get; }
    }
}
