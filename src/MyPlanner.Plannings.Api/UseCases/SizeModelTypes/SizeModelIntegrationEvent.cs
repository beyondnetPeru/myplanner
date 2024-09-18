using MyPlanner.Plannings.EventBus.Events;

namespace MyPlanner.Plannings.Api.UseCases.SizeModelTypes
{
    public record OrderStartedIntegrationEvent : IntegrationEvent
    {
        public string UserId { get; init; }

        public OrderStartedIntegrationEvent(string userId)
            => UserId = userId;
    }

}
