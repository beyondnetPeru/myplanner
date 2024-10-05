using MyPlanner.Plannings.Api.Services.Interfaces;
using MyPlanner.EventBus.Extensions;
using MyPlanner.Plannings.Infrastructure.Database;

namespace MyPlanner.Plannings.Api.Behaviors
{
    /// <summary>
    /// Represents a behavior that handles transactions for pipeline requests.
    /// </summary>
    /// <typeparam name="TRequest">The type of the request.</typeparam>
    /// <typeparam name="TResponse">The type of the response.</typeparam>
    public class TransactionBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly ILogger<TransactionBehavior<TRequest, TResponse>> logger;
        private readonly PlanningDbContext dbContext;
        private readonly IPlanningIntegrationEventService planningIntegrationEventService;

        /// <summary>
        /// Initializes a new instance of the <see cref="TransactionBehavior{TRequest, TResponse}"/> class.
        /// </summary>
        /// <param name="dbContext">The planning database context.</param>
        /// <param name="orderingIntegrationEventService">The planning integration event service.</param>
        /// <param name="logger">The logger.</param>
        public TransactionBehavior(PlanningDbContext dbContext,
            IPlanningIntegrationEventService orderingIntegrationEventService,
            ILogger<TransactionBehavior<TRequest, TResponse>> logger)
        {
            this.dbContext = dbContext ?? throw new ArgumentException(nameof(dbContext));
            planningIntegrationEventService = orderingIntegrationEventService ?? throw new ArgumentException(nameof(orderingIntegrationEventService));
            this.logger = logger ?? throw new ArgumentException(nameof(ILogger));
        }

        /// <summary>
        /// Handles the pipeline request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="next">The next handler delegate.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The response.</returns>
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var response = default(TResponse);
            var typeName = request.GetGenericTypeName();

            try
            {
                if (dbContext.HasActiveTransaction)
                {
                    return await next();
                }

                var strategy = dbContext.Database.CreateExecutionStrategy();

                await strategy.ExecuteAsync(async () =>
                {
                    Guid transactionId;

                    await using var transaction = await dbContext.BeginTransactionAsync();
                    using (logger.BeginScope(new List<KeyValuePair<string, object>> { new("TransactionContext", transaction.TransactionId) }))
                    {
                        logger.LogInformation("Begin transaction {TransactionId} for {CommandName} ({@Command})", transaction.TransactionId, typeName, request);

                        response = await next();

                        logger.LogInformation("Commit transaction {TransactionId} for {CommandName}", transaction.TransactionId, typeName);

                        await dbContext.CommitTransactionAsync(transaction);

                        transactionId = transaction.TransactionId;
                    }

                    await planningIntegrationEventService.PublishEventsThroughEventBusAsync(transactionId);
                });

                return response!; // Add null-forgiving operator to indicate that response is not null
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error Handling transaction for {CommandName} ({@Command})", typeName, request);

                throw;
            }
        }
    }
}
