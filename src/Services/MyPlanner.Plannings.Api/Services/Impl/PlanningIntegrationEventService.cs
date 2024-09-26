using MyPlanner.EventBus.Events;
using MyPlanner.IntegrationEventLogEF.Services;
using MyPlanner.Plannings.Api.Services.Interfaces;
using MyPlanner.Plannings.Infrastructure.Database;


namespace MyPlanner.Plannings.Api.Services.Impl
{
    public class PlanningIntegrationEventService : IPlanningIntegrationEventService
    {
        //private readonly IEventBus eventBus;
        private readonly PlanningDbContext planningDbContext;
        private readonly IIntegrationEventLogService integrationEventLogService;
        private readonly ILogger<PlanningIntegrationEventService> logger;

        public PlanningIntegrationEventService(/*IEventBus eventBus,*/
                                              PlanningDbContext planningDbContext,
                                              IIntegrationEventLogService integrationEventLogService,
                                              ILogger<PlanningIntegrationEventService> logger)
        {
            //this.eventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));
            this.planningDbContext = planningDbContext ?? throw new ArgumentNullException(nameof(PlanningDbContext));
            this.integrationEventLogService = integrationEventLogService ?? throw new ArgumentNullException(nameof(integrationEventLogService));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task PublishEventsThroughEventBusAsync(Guid transactionId)
        {
            var pendingLogEvents = await integrationEventLogService.RetrieveEventLogsPendingToPublishAsync(transactionId);

            foreach (var logEvt in pendingLogEvents)
            {
                logger.LogInformation("Publishing integration event: {IntegrationEventId} - ({@IntegrationEvent})", logEvt.EventId, logEvt.IntegrationEvent);

                try
                {
                    await integrationEventLogService.MarkEventAsInProgressAsync(logEvt.EventId);
                    //await eventBus.PublishAsync(logEvt.IntegrationEvent);
                    await integrationEventLogService.MarkEventAsPublishedAsync(logEvt.EventId);
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Error publishing integration event: {IntegrationEventId}", logEvt.EventId);

                    await integrationEventLogService.MarkEventAsFailedAsync(logEvt.EventId);
                }
            }
        }

        public async Task AddAndSaveEventAsync(IntegrationEvent evt)
        {
            logger.LogInformation("----- Enqueuing integration event {IntegrationEventId} to repository ({@IntegrationEvent})", evt.Id, evt);

            await integrationEventLogService.SaveEventAsync(evt, planningDbContext.GetCurrentTransaction());
        }

    }
}
