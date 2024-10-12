namespace MyPlanner.Plannings.Api.UseCases.SizeModelTypes.Queries.GetSizeModelTypeFactor
{
    public class GetSizeModelTypeItemQuery : IQuery<ResultSet>
    {
        public GetSizeModelTypeItemQuery(string sizeModelTypeId, string sizeModelTypeItemId)
        {
            SizeModelTypeId = sizeModelTypeId;
            SizeModelTypeItemId = sizeModelTypeItemId;
        }

        public string SizeModelTypeId { get; }
        public string SizeModelTypeItemId { get; }
    }
}
