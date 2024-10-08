using MyPlanner.Shared.Cqrs.Interfaces;

namespace MyPlanner.Shared.Cqrs
{
    public abstract class AbstractQueryHandler<TQuery, TResult> : IQueryHandler<TQuery, TResult>
            where TQuery : IQuery<TResult>
            where TResult : ResultSet
    {
        private readonly ILogger<AbstractQueryHandler<TQuery, TResult>> logger;

        public AbstractQueryHandler(ILogger<AbstractQueryHandler<TQuery, TResult>> logger)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<TResult> Handle(TQuery request, CancellationToken cancellationToken)
        {
            try
            {
                logger.LogInformation($"Handling query {request.GetType().Name} with request: {JsonSerializer.Serialize(request)}");

                var result = await HandleQuery(request, cancellationToken);

                logger.LogInformation($"Query {request.GetType().Name} handled with response: {JsonSerializer.Serialize(result)}");

                return result;
            }
            catch (Exception error)
            {
                logger.LogError($"Error: {error.Message}", $"Error handling command {request.GetType().Name} with request: {JsonSerializer.Serialize(request)}. Error Details: {JsonSerializer.Serialize(error)}");
                throw;
            }
            finally
            {
                logger.LogInformation($"Command {request.GetType().Name} finished request");
            }
        }

        public abstract Task<TResult> HandleQuery(TQuery request, CancellationToken cancellationToken);
    }
}
