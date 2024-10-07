using MyPlanner.Shared.Cqrs;
using MyPlanner.Shared.Cqrs.Interfaces;

namespace MyPlanner.Plannings.Api.UseCases.Plan.Command.ChangeName
{
    public class ChangePlanNameCommand : ICommand<ResultSet>
    {
        public string PlanId { get; set; }
        public string Name { get; set; }

        public ChangePlanNameCommand(string planId, string name)
        {
            PlanId = planId;
            Name = name;
        }
    }
}
