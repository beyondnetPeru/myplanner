using MyPlanner.Plannings.Domain.SizeModels;
using MyPlanner.Shared.Domain.ValueObjects;

namespace MyPlanner.Plannings.Api.UseCases.SizeModels.Command.DeactivateSizeModelItem
{
    public class DeactivateSizeModelItemRequestHandler : IRequestHandler<DeactivateSizeModelItemRequest, bool>
    {
        private readonly ISizeModelRepository sizeModelRepository;
        private readonly ILogger<DeactivateSizeModelItemRequestHandler> logger;

        public DeactivateSizeModelItemRequestHandler(ISizeModelRepository sizeModelRepository, ILogger<DeactivateSizeModelItemRequestHandler> logger)
        {
            this.sizeModelRepository = sizeModelRepository;
            this.logger = logger;
        }

        public async Task<bool> Handle(DeactivateSizeModelItemRequest request, CancellationToken cancellationToken)
        {
            var sizeModelItem = await sizeModelRepository.GetItem(request.SizeModelItemId);

            sizeModelItem.Deactivate(UserId.Create(request.UserId));

            if (!sizeModelItem.IsValid())
            {
                logger.LogError($"SizeModelItem is not valid. Errors:{sizeModelItem.GetBrokenRules()}");
                return false;
            }

            sizeModelRepository.DeactiveItem(request.SizeModelItemId);

            await sizeModelRepository.UnitOfWork.SaveEntitiesAsync(this, cancellationToken);

            return true;
        }
    }
}
