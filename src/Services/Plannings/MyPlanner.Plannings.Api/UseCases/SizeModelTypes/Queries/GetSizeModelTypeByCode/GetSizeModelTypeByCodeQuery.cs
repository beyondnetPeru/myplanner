


namespace MyPlanner.Plannings.Api.UseCases.SizeModelTypes.Queries.GetSizeModelTypeByCode
{
    public class GetSizeModelTypeByCodeQuery : IQuery<ResultSet>
    {
        public GetSizeModelTypeByCodeQuery(string code)
        {
            Code = code;
        }

        public string Code { get; }
    }
}
