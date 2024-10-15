using MyPlanner.EventBus.Abstractions;

namespace MyPlanner.Plannings.Api.UseCases.Plan.IntegrationEvents
{
    public class PlanDraftCreatedIntegrationEventHandler(IMediator mediator, 
                                                         ILogger<PlanDraftCreatedIntegrationEventHandler> logger) : IIntegrationEventHandler<PlanDraftCreatedIntegrationEvent>
    {
        public async Task Handle(PlanDraftCreatedIntegrationEvent @event)
        {
            logger.LogInformation("Handling integration event: {IntegrationEventId} - ({@IntegrationEvent})", @event.Id, @event);
        }
    }
}
