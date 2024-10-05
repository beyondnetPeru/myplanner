using MyPlanner.Shared.Cqrs;
using MyPlanner.Shared.Cqrs.Interfaces;

namespace MyPlanner.Plannings.Api.UseCases.SizeModels.Queries.GetAllSizeModelItems
{
    public class GetAllSizeModelItemQuery : IQuery<ResultSet>
    {
        public GetAllSizeModelItemQuery(string sizeModelId)
        {
            SizeModelId = sizeModelId;
        }

        public string SizeModelId { get; }
    }
}
