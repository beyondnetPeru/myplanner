using MyPlanner.Plannings.Domain.SizeModels;
using MyPlanner.Plannings.Shared.Domain.ValueObjects;

namespace MyPlanner.Plannings.Api.UseCases.SizeModels.Command.ChangeFactorSizeModel
{
    public class ChangeFactorSizeModelItemRequestHandler : IRequestHandler<ChangeFactorSizeModelItemRequest, bool>
    {
        private readonly ISizeModelRepository sizeModelRepository;
        private readonly ILogger<ChangeFactorSizeModelItemRequestHandler> logger;

        public ChangeFactorSizeModelItemRequestHandler(ISizeModelRepository sizeModelRepository, ILogger<ChangeFactorSizeModelItemRequestHandler> logger)
        {
            this.sizeModelRepository = sizeModelRepository ?? throw new ArgumentNullException(nameof(sizeModelRepository));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<bool> Handle(ChangeFactorSizeModelItemRequest request, CancellationToken cancellationToken)
        {
            var sizeModelItem = await sizeModelRepository.GetItem(request.SizeModelItemId);

            sizeModelItem.ChangeFactorSelected(Enumeration.FromValue<FactorsEnum>(request.FactorSelected), UserId.Create(request.UserId));

            if (!sizeModelItem.IsValid())
            {
                logger.LogError($"SizeModelItem is not valid: {sizeModelItem.GetBrokenRules()}");
                return false;
            }

            sizeModelRepository.ChangeFactorSelected(request.SizeModelItemId, request.FactorSelected);

            await sizeModelRepository.UnitOfWork.SaveEntitiesAsync(sizeModelItem, cancellationToken);

            return true;
        }
    }
}
