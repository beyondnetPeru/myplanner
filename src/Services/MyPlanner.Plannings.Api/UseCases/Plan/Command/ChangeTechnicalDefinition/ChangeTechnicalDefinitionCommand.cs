using MyPlanner.Shared.Cqrs;
using MyPlanner.Shared.Cqrs.Interfaces;

namespace MyPlanner.Plannings.Api.UseCases.Plan.Command.ChangeTechnicalDefinitionPlanItem
{
    public class ChangeTechnicalDefinitionCommand : ICommand<ResultSet>
    {
        public string PlanItemId { get; set; }
        public string TechnicalDefinition { get; set; }
        public string UserId { get; set; }

        public ChangeTechnicalDefinitionCommand(string planItemId, string technicalDefinition, string userId)
        {
            PlanItemId = planItemId;
            TechnicalDefinition = technicalDefinition;
            UserId = userId;
        }
    }
}
