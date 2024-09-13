using MediatR;
using MyPlanner.Plannings.Api.UseCases.SizeModels.ActivateSizeModel;
using MyPlanner.Plannings.Api.UseCases.SizeModels.ActivateSizeModelItem;
using MyPlanner.Plannings.Api.UseCases.SizeModels.ChangeSizeModelName;
using MyPlanner.Plannings.Api.UseCases.SizeModels.CreateSizeModel;
using MyPlanner.Plannings.Api.UseCases.SizeModels.CreateSizeModelItem;
using MyPlanner.Plannings.Api.UseCases.SizeModels.DeactivateSizeModel;
using MyPlanner.Plannings.Api.UseCases.SizeModels.DeactivateSizeModelItem;
using MyPlanner.Plannings.Api.UseCases.SizeModelTypes.ActivateSizeModelType;
using MyPlanner.Plannings.Api.UseCases.SizeModelTypes.ActivateSizeModelTypeFactor;
using MyPlanner.Plannings.Api.UseCases.SizeModelTypes.AddSizeModelTypeFactor;
using MyPlanner.Plannings.Api.UseCases.SizeModelTypes.ChangeCodeSizeModelType;
using MyPlanner.Plannings.Api.UseCases.SizeModelTypes.ChangeNameSizeModelType;
using MyPlanner.Plannings.Api.UseCases.SizeModelTypes.CreateSizeModelType;
using MyPlanner.Plannings.Api.UseCases.SizeModelTypes.DeactivateSizeModelType;
using MyPlanner.Plannings.Api.UseCases.SizeModelTypes.DeactivateSizeModelTypeFactor;
using MyPlanner.Plannings.Shared.Application.Commands;
using MyPlanner.Plannings.Shared.Application.Extensions;
using MyPlanner.Plannings.Shared.Infrastructure.Idempotency;

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
                        case CreateSizeModelTypeRequest createSizeModelTypeRequest:
                            idProperty = nameof(createSizeModelTypeRequest.UserId);
                            commandId = createSizeModelTypeRequest.UserId;
                            break;

                        case ChangeCodeSizeModelTypeRequest changeCodeSizeModelTypeRequest:
                            idProperty = nameof(changeCodeSizeModelTypeRequest.UserId);
                            commandId = changeCodeSizeModelTypeRequest.UserId;
                            break;

                        case ChangeNameSizeModelTypeRequest changeNameSizeModelTypeRequest:
                            idProperty = nameof(changeNameSizeModelTypeRequest.UserId);
                            commandId = changeNameSizeModelTypeRequest.UserId;
                            break;

                        case ActivateSizeModelTypeRequest activateSizeModelTypeRequest:
                            idProperty = nameof(activateSizeModelTypeRequest.UserId);
                            commandId = activateSizeModelTypeRequest.UserId;
                            break;

                        case DeactivateSizeModelTypeRequest deactivateSizeModelTypeRequest:
                            idProperty = nameof(deactivateSizeModelTypeRequest.UserId);
                            commandId = deactivateSizeModelTypeRequest.UserId;
                            break;

                        case AddSizeModelTypeFactorRequest addSizeModelTypeFactorRequest:
                            idProperty = nameof(addSizeModelTypeFactorRequest.UserId);
                            commandId = addSizeModelTypeFactorRequest.UserId;
                            break;

                        case ActivateSizeModelTypeFactorRequest activateSizeModelTypeFactorRequest:
                            idProperty = nameof(activateSizeModelTypeFactorRequest.UserId);
                            commandId = activateSizeModelTypeFactorRequest.UserId;
                            break;

                        case DeactivateSizeModelTypeFactorRequest deactivateSizeModelTypeFactorRequest:
                            idProperty = nameof(deactivateSizeModelTypeFactorRequest.UserId);
                            commandId = deactivateSizeModelTypeFactorRequest.UserId;
                            break;

                        case ActivateSizeModelRequest activateSizeModelRequest:
                            idProperty = nameof(activateSizeModelRequest.UserId);
                            commandId = activateSizeModelRequest.UserId;
                            break;

                        case DeactivateSizeModelRequest deactivateSizeModelRequest:
                            idProperty = nameof(deactivateSizeModelRequest.UserId);
                            commandId = deactivateSizeModelRequest.UserId;
                            break;

                        case CreateSizeModelRequest createSizeModelRequest:
                            idProperty = nameof(createSizeModelRequest.UserId);
                            commandId = createSizeModelRequest.UserId;
                            break;

                        case ChangeSizeModelNameRequest changeSizeModelNameRequest:
                            idProperty = nameof(changeSizeModelNameRequest.UserId);
                            commandId = changeSizeModelNameRequest.UserId;
                            break;

                        case CreateSizeModelItemRequest createSizeModelItemRequest:
                            idProperty = nameof(createSizeModelItemRequest.UserId);
                            commandId = createSizeModelItemRequest.UserId;
                            break;

                        case DeactivateSizeModelItemRequest deactivateSizeModelItemRequest:
                            idProperty = nameof(deactivateSizeModelItemRequest.UserId);
                            commandId = deactivateSizeModelItemRequest.UserId;
                            break;

                        case ActivateSizeModelItemRequest activateSizeModelItemRequest:
                            idProperty = nameof(activateSizeModelItemRequest.UserId);
                            commandId = activateSizeModelItemRequest.UserId;
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
