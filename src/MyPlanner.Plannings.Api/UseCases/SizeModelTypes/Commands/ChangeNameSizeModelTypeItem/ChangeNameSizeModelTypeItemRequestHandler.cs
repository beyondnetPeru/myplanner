using MyPlanner.Plannings.Api.Endpoints;

namespace MyPlanner.Plannings.Api.UseCases.SizeModelTypes.Commands.ChangeNameSizeModelTypeItem
{
    public class ChangeNameSizeModelTypeItemRequestHandler : IRequestHandler<ChangeNameSizeModelTypeItemRequest, bool>
    {
        private readonly ISizeModelTypeRepository sizeModelTypeRepository;
        private readonly ILogger<ChangeNameSizeModelTypeItemRequestHandler> logger;

        public ChangeNameSizeModelTypeItemRequestHandler(ISizeModelTypeRepository sizeModelTypeRepository,
                                                         ILogger<ChangeNameSizeModelTypeItemRequestHandler> logger)
        {
            this.sizeModelTypeRepository = sizeModelTypeRepository ?? throw new ArgumentNullException(nameof(sizeModelTypeRepository));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<bool> Handle(ChangeNameSizeModelTypeItemRequest request, CancellationToken cancellationToken)
        {
            var sizeModelTypeItem = await sizeModelTypeRepository.GetItemById(request.SizeModelTypeItemId);

            sizeModelTypeItem.GetProps().Name.SetValue(request.Name);

            if (!sizeModelTypeItem.IsValid())
            {
                logger.LogError($"Invalid SizeModelTypeItem: Errors {sizeModelTypeItem.GetBrokenRules().ToString()}");
                return true;
            }

            sizeModelTypeRepository.ChangeItemName(request.SizeModelTypeItemId, request.Name);

            await sizeModelTypeRepository.UnitOfWork.SaveEntitiesAsync(sizeModelTypeItem, cancellationToken);

            return true;

        }
    }
}
