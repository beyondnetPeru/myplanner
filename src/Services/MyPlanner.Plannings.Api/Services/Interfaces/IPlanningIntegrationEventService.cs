using MyPlanner.Plannings.EventBus.Events;

namespace MyPlanner.Plannings.Api.Services.Interfaces
{
    public interface IPlanningIntegrationEventService
    {
        Task PublishEventsThroughEventBusAsync(Guid transactionId);
        Task AddAndSaveEventAsync(IntegrationEvent evt);
    }
}
