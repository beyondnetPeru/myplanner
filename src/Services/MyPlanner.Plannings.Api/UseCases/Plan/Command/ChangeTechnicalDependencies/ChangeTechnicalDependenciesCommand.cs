using MyPlanner.Shared.Cqrs;
using MyPlanner.Shared.Cqrs.Interfaces;

namespace MyPlanner.Plannings.Api.UseCases.Plan.Command.ChangeTechnicalDependencies
{
    public class ChangeTechnicalDependenciesCommand : ICommand<ResultSet>
    {
        public string PlanItemId { get; set; }
        public string TechnicalDependencies { get; set; }
        public string UserId { get; set; }

        public ChangeTechnicalDependenciesCommand(string planItemId, string technicalDependencies, string userId)
        {
            PlanItemId = planItemId;
            TechnicalDependencies = technicalDependencies;
            UserId = userId;
        }
    }
}
