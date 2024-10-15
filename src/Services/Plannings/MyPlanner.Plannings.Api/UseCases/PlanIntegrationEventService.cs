using MyPlanner.EventBus.Abstractions;
using MyPlanner.EventBus.Events;
using MyPlanner.IntegrationEventLogEF.Services;
using MyPlanner.Plannings.Infrastructure.Database;
using MyPlanner.Shared.Services.IntegrationEvents;

namespace MyPlanner.Plannings.Api.UseCases
{
    public class PlanIntegrationEventService(IEventBus eventBus,
        PlanningDbContext planningDbContext,
        IIntegrationEventLogService integrationEventLogService,
        ILogger<PlanIntegrationEventService> logger)
        : IIntegrationEventService
    {
        private readonly IEventBus _eventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));
        private readonly PlanningDbContext _planningDbContext = planningDbContext ?? throw new ArgumentNullException(nameof(planningDbContext));
        private readonly IIntegrationEventLogService _eventLogService = integrationEventLogService ?? throw new ArgumentNullException(nameof(integrationEventLogService));
        private readonly ILogger<PlanIntegrationEventService> _logger = logger ?? throw new ArgumentNullException(nameof(logger));

        public async Task AddAndSaveEventAsync(IntegrationEvent evt)
        {
            _logger.LogInformation("Enqueuing integration event {IntegrationEventId} to repository ({@IntegrationEvent})", evt.Id, evt);

            await _eventLogService.SaveEventAsync(evt, _planningDbContext.GetCurrentTransaction());
        }

        public async Task PublishEventsThroughEventBusAsync(Guid transactionId)
        {
            var pendingLogEvents = await _eventLogService.RetrieveEventLogsPendingToPublishAsync(transactionId);

            foreach (var logEvt in pendingLogEvents)
            {
                _logger.LogInformation("Publishing integration event: {IntegrationEventId} - ({@IntegrationEvent})", logEvt.EventId, logEvt.IntegrationEvent);

                try
                {
                    await _eventLogService.MarkEventAsInProgressAsync(logEvt.EventId);
                    await _eventBus.PublishAsync(logEvt.IntegrationEvent);
                    await _eventLogService.MarkEventAsPublishedAsync(logEvt.EventId);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error publishing integration event: {IntegrationEventId}", logEvt.EventId);

                    await _eventLogService.MarkEventAsFailedAsync(logEvt.EventId);
                }
            }
        }
    }
}
