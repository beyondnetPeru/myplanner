


namespace MyPlanner.Plannings.Api.UseCases.Plan.Queries.GetPlanItem
{
    public class GetPlanItemQuery : IQuery<ResultSet>
    {
        public GetPlanItemQuery(string planId, string planItemId)
        {
            PlanId = planId;
            PlanItemId = planItemId;
        }

        public string PlanId { get; }
        public string PlanItemId { get; }
    }
}
