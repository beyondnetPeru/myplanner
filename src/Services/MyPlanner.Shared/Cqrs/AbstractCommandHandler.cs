using MyPlanner.Shared.Cqrs.Interfaces;

namespace MyPlanner.Shared.Cqrs
{
    public abstract class AbstractCommandHandler<TCommand, TResult> : ICommandHandler<TCommand, TResult>
            where TCommand : ICommand<TResult>
            where TResult : ResultSet
    {
        private readonly ILogger<AbstractCommandHandler<TCommand, TResult>> logger;

        public AbstractCommandHandler(ILogger<AbstractCommandHandler<TCommand, TResult>> Logger)
        {
            logger = Logger;
        }

        public async Task<TResult> Handle(TCommand request, CancellationToken cancellationToken)
        {
            try
            {
                logger.LogInformation($"Handling command {request.GetType().Name} with request: {JsonSerializer.Serialize(request)}");

                var result = await HandleCommand(request, cancellationToken);

                logger.LogInformation($"Command {request.GetType().Name} handled with response: {JsonSerializer.Serialize(result)}");

                return result;
            }
            catch (Exception error)
            {
                logger.LogError($"Error: {error.Message}", $"Error handling command {request.GetType().Name} with request: {JsonSerializer.Serialize(request)}. Error Details: {JsonSerializer.Serialize(error)}");
                throw; // rethrow the exception
            }
            finally
            {
                logger.LogInformation($"Command {request.GetType().Name} finished request");
            }
        }
       
        public abstract Task<TResult> HandleCommand(TCommand request, CancellationToken cancellationToken);
      
    }
}
