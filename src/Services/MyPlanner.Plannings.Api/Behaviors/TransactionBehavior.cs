using Microsoft.EntityFrameworkCore;
using MyPlanner.Plannings.Api.Services.Interfaces;
using MyPlanner.EventBus.Extensions;
using MyPlanner.Plannings.Infrastructure.Database;

namespace MyPlanner.Plannings.Api.Behaviors
{
    public class TransactionBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly ILogger<TransactionBehavior<TRequest, TResponse>> _logger;
        private readonly PlanningDbContext dbContext;
        private readonly IPlanningIntegrationEventService planningIntegrationEventService;

        public TransactionBehavior(PlanningDbContext dbContext,
            IPlanningIntegrationEventService orderingIntegrationEventService,
            ILogger<TransactionBehavior<TRequest, TResponse>> logger)
        {
            this.dbContext = dbContext ?? throw new ArgumentException(nameof(dbContext));
            planningIntegrationEventService = orderingIntegrationEventService ?? throw new ArgumentException(nameof(orderingIntegrationEventService));
            _logger = logger ?? throw new ArgumentException(nameof(ILogger));
        }

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
                    using (_logger.BeginScope(new List<KeyValuePair<string, object>> { new("TransactionContext", transaction.TransactionId) }))
                    {
                        _logger.LogInformation("Begin transaction {TransactionId} for {CommandName} ({@Command})", transaction.TransactionId, typeName, request);

                        response = await next();

                        _logger.LogInformation("Commit transaction {TransactionId} for {CommandName}", transaction.TransactionId, typeName);

                        await dbContext.CommitTransactionAsync(transaction);

                        transactionId = transaction.TransactionId;
                    }

                    await planningIntegrationEventService.PublishEventsThroughEventBusAsync(transactionId);
                });

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error Handling transaction for {CommandName} ({@Command})", typeName, request);

                throw;
            }
        }
    }
}
