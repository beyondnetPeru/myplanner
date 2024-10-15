namespace MyPlanner.Plannings.Api.UseCases.Plan.Queries.GetPlanByCode
{
    public class GetPlanByCodeQuery : IQuery<ResultSet>
    {
        public string Code { get; set; } = default!;

        public GetPlanByCodeQuery(string code)
        {
            Code = code;
        }
    }
}
