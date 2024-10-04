using MyPlanner.Shared.Cqrs.Interfaces;

namespace MyPlanner.Shared.Cqrs
{
    public abstract class AbstractQueryHandler<TQuery, TResponse> : IQueryHandler<TQuery, ResultSet>
            where TQuery : IQuery<TResponse>
            where TResponse : ResultSet
    {
        private readonly ILogger<AbstractQueryHandler<TQuery, TResponse>> logger;

        public AbstractQueryHandler(ILogger<AbstractQueryHandler<TQuery, TResponse>> logger)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<ResultSet> Handle(TQuery request, CancellationToken cancellationToken)
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

        public abstract Task<TResponse> HandleQuery(TQuery request, CancellationToken cancellationToken);
    }
}
