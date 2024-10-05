using MyPlanner.Shared.Cqrs;
using MyPlanner.Shared.Cqrs.Interfaces;

namespace MyPlanner.Plannings.Api.UseCases.SizeModels.Queries.GetSizeModelItem
{
    public class GetSizeModelItemQuery : IQuery<ResultSet>
    {

        public GetSizeModelItemQuery(string sizeModelItemId)
        {
            SizeModelItemId = sizeModelItemId;
        }

        public string SizeModelItemId { get; }
    }
}
