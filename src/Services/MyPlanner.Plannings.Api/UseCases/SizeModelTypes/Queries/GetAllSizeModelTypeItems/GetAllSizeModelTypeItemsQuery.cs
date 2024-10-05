using MyPlanner.Shared.Cqrs;
using MyPlanner.Shared.Cqrs.Interfaces;

namespace MyPlanner.Plannings.Api.UseCases.SizeModelTypes.Queries.GetAllSizeModelTypeFactors
{
    public class GetAllSizeModelTypeItemsQuery : IQuery<ResultSet>
    {
        public GetAllSizeModelTypeItemsQuery(string sizeModelTypeId)
        {
            SizeModelTypeId = sizeModelTypeId;
        }

        public string SizeModelTypeId { get; }
    }
}
