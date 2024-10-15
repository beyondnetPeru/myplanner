


namespace MyPlanner.Plannings.Api.UseCases.SizeModels.Queries.GetSizeModel
{
    public class GetSizeModelQuery : IQuery<ResultSet>
    {
        public string SizeModelId { get; set; }

        public GetSizeModelQuery(string sizeModelId)
        {
            SizeModelId = sizeModelId;
        }
    }
}
