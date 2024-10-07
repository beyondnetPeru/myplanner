using MyPlanner.Shared.Cqrs.Interfaces;
using MyPlanner.Shared.Extensions;

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
                var handlingCommand = request.GetGenericTypeName();

                logger.LogInformation($"Start handling command {handlingCommand} with request: {JsonSerializer.Serialize(request)}");

                var result = await HandleCommand(request, cancellationToken);

                logger.LogInformation($"End handling Command {handlingCommand} handled with response: {JsonSerializer.Serialize(result)}");

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
