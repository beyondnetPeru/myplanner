


namespace MyPlanner.Plannings.Api.UseCases.SizeModelTypes.Queries.GetAllSizeModelTypes
{
    public class GetAllSizeModelTypesQuery : IQuery<ResultSet>
    {
        public GetAllSizeModelTypesQuery(PaginationQuery pagination)
        {
            Pagination = pagination;
        }

        public PaginationQuery Pagination { get; }
    }
}
