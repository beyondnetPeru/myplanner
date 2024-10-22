using MyPlanner.Plannings.Api.UseCases.Plan.Command.ActivatePlan;
using MyPlanner.Plannings.Api.UseCases.Plan.Command.ChangeName;
using MyPlanner.Plannings.Api.UseCases.Plan.Command.ChangeOwner;
using MyPlanner.Plannings.Api.UseCases.Plan.Command.ChangePlanItemType;
using MyPlanner.Plannings.Api.UseCases.Plan.Command.ClosePlan;
using MyPlanner.Plannings.Api.UseCases.Plan.Command.CreatePlan;
using MyPlanner.Plannings.Api.UseCases.Plan.Command.DeactivatePlan;
using MyPlanner.Plannings.Api.UseCases.Plan.Command.DeletePlan;
using MyPlanner.Plannings.Api.UseCases.Plan.Command.DraftPlan;

using MyPlanner.Shared.Extensions;
using MyPlanner.Shared.Infrastructure.Idempotency;

namespace MyPlanner.Plannings.Api.UseCases
{
    /// <summary>
    /// Provides a base implementation for handling duplicate request and ensuring idempotent updates, in the cases where
    /// a requestid sent by client is used to detect duplicate requests.
    /// </summary>
    /// <typeparam name="T">Type of the command handler that performs the operation if request is not duplicated</typeparam>
    /// <typeparam name="R">Return value of the inner command handler</typeparam>
    public abstract class IdentifiedCommandHandler<T, R> : IRequestHandler<IdentifiedCommand<T, R>, R>
        where T : IRequest<R>
        where R : ResultSet
    {
        private readonly IMediator mediator;
        private readonly IRequestManager requestManager;
        private readonly ILogger<IdentifiedCommandHandler<T, R>> logger;

        public IdentifiedCommandHandler(
            IMediator mediator,
            IRequestManager requestManager,
            ILogger<IdentifiedCommandHandler<T, R>> logger)
        {
            ArgumentNullException.ThrowIfNull(logger);
            this.mediator = mediator;
            this.requestManager = requestManager;
            this.logger = logger;
        }

        /// <summary>
        /// Creates the result value to return if a previous request was found
        /// </summary>
        /// <returns></returns>
        protected abstract R CreateResultForDuplicateRequest();

        /// <summary>
        /// This method handles the command. It just ensures that no other request exists with the same ID, and if this is the case
        /// just enqueues the original inner command.
        /// </summary>
        /// <param name="message">IdentifiedCommand which contains both original command & request ID</param>
        /// <returns>Return value of inner command or default value if request same ID was found</returns>

        //TODO: Refactor this method to use a switch statement to handle the different types of commands using MatchExpression or Matching in C# 9
        // https://docs.microsoft.com/en-us/dotnet/csharp/whats-new/csharp-9#matching
        // https://medium.com/@nirajranasinghe/pattern-matching-in-c-fcee69929776
        // https://antondevtips.com/blog/how-to-write-elegant-code-with-csharp-pattern-matching

        public async Task<R> Handle(IdentifiedCommand<T, R> message, CancellationToken cancellationToken)
        {
            var alreadyExists = await requestManager.ExistAsync(message.Id);
            if (alreadyExists)
            {
                return CreateResultForDuplicateRequest();
            }
            else
            {
                await requestManager.CreateRequestForCommandAsync<T>(message.Id);
                try
                {
                    var command = message.Command;
                    var commandName = command.GetGenericTypeName();
                    var idProperty = string.Empty;
                    var commandId = string.Empty;

                    switch (command)
                    {
                        case CreatePlanCommand createPlanRequest:
                            idProperty = nameof(createPlanRequest.UserId);
                            commandId = createPlanRequest.UserId;
                            break;

                        case ActivatePlanCommand activatePlanRequest:
                            idProperty = nameof(activatePlanRequest.UserId);
                            commandId = activatePlanRequest.UserId;
                            break;

                        case ChangePlanNameCommand changePlanNameRequest:
                            idProperty = nameof(changePlanNameRequest.UserId);
                            commandId = changePlanNameRequest.UserId;
                            break;

                        case ChangePlanOwnerCommand changePlanOwnerRequest:
                            idProperty = nameof(changePlanOwnerRequest.UserId);
                            commandId = changePlanOwnerRequest.UserId;
                            break;

                        case ClosePlanCommand closePlanRequest:
                            idProperty = nameof(closePlanRequest.UserId);
                            commandId = closePlanRequest.UserId;
                            break;

                        case DeactivatePlanCommand deactivatePlanRequest:
                            idProperty = nameof(deactivatePlanRequest.UserId);
                            commandId = deactivatePlanRequest.UserId;
                            break;

                        case DeletePlanCommand deletePlanRequest:
                            idProperty = nameof(deletePlanRequest.UserId);
                            commandId = deletePlanRequest.UserId;
                            break;

                        case DraftPlanCommand draftPlanCommand:
                            idProperty = nameof(draftPlanCommand.UserId);
                            commandId = draftPlanCommand.UserId;
                            break;
                        case ChangePlanItemTypeCommand changePlanItemTypeRequest:
                            idProperty = nameof(changePlanItemTypeRequest.UserId);
                            commandId = changePlanItemTypeRequest.UserId;
                            break;

                        default:
                            idProperty = "Id?";
                            commandId = "n/a";
                            break;
                    }

                    logger.LogInformation(
                        "Sending command: {CommandName} - {IdProperty}: {CommandId} ({@Command})",
                        commandName,
                        idProperty,
                        commandId,
                        command);

                    // Send the embedded business command to mediator so it runs its related CommandHandler 
                    var result = await mediator.Send(command, cancellationToken);

                    logger.LogInformation(
                        "Command result: {@Result} - {CommandName} - {IdProperty}: {CommandId} ({@Command})",
                        result,
                        commandName,
                        idProperty,
                        commandId,
                        command);

                    return result;
                }
                catch
                {
                    return default!;
                }
            }
        }
    }
}
