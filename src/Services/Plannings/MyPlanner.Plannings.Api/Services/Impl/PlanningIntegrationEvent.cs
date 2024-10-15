using MyPlanner.EventBus.Events;

namespace MyPlanner.Plannings.Api.Services.Impl
{
    public record PlanningIntegrationEvent : IntegrationEvent
    {
        public PlanningIntegrationEvent(string userId)
        {
            UserId = userId;
        }

        public string UserId { get; }
    }
}
