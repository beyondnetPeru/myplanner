using MyPlanner.EventBus.Events;

namespace MyPlanner.Plannings.Api.UseCases.Plan.IntegrationEvents
{
    public record PlanDraftCreatedIntegrationEvent : IntegrationEvent
    {
        public string PlanId { get; }
        public string PlanCode { get; }
        public string PlanName { get; }
        public string PlanStatus { get; }

        public PlanDraftCreatedIntegrationEvent(string planId, string planCode, string planName)
        {
            PlanId = planId;
            PlanCode = planCode;
            PlanName = planName;
            PlanStatus = "Draft";
        }            
    }
}
