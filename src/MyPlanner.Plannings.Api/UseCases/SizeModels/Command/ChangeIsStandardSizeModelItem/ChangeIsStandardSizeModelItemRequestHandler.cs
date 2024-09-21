using MyPlanner.Plannings.Api.UseCases.SizeModels.Command.ChangeQuantitySizeModelItem;
using MyPlanner.Plannings.Domain.SizeModels;
using MyPlanner.Plannings.Shared.Domain.ValueObjects;

namespace MyPlanner.Plannings.Api.UseCases.SizeModels.Command.ChangeIsStandardSizeModelItem
{
    public class ChangeIsStandardSizeModelItemRequestHandler : IRequestHandler<ChangeIsStandardSizeModelItemRequest, bool>
    {
        private readonly ISizeModelRepository sizeModelRepository;
        private readonly ILogger<ChangeIsStandardSizeModelItemRequestHandler> logger;

        public ChangeIsStandardSizeModelItemRequestHandler(ISizeModelRepository sizeModelRepository, ILogger<ChangeIsStandardSizeModelItemRequestHandler> logger)
        {
            this.sizeModelRepository = sizeModelRepository ?? throw new ArgumentNullException(nameof(sizeModelRepository));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<bool> Handle(ChangeIsStandardSizeModelItemRequest request, CancellationToken cancellationToken)
        {
            var sizeModelItem = await sizeModelRepository.GetItem(request.SizeModelItemId);

            sizeModelItem.ChangeIsStandard(request.IsStandard, UserId.Create(request.UserId));

            if (!sizeModelItem.IsValid())
            {
                logger.LogError($"Invalid size model item. Errors: {sizeModelItem.GetBrokenRules()}");
                return false;
            }

            sizeModelRepository.ChangeIsStandard(request.SizeModelItemId, request.IsStandard);

            await sizeModelRepository.UnitOfWork.SaveEntitiesAsync(sizeModelItem, cancellationToken);

            return true;
        }
    }
}
