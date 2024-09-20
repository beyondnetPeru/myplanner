using MyPlanner.Plannings.Api.Endpoints;
using MyPlanner.Plannings.Domain.SizeModels;

namespace MyPlanner.Plannings.Api.UseCases.SizeModelTypes.Commands.ChangeCodeSizeModelTypeItem
{
    public class ChangeCodeSizeModelTypeItemRequestHandler : IRequestHandler<ChangeCodeSizeModelTypeItemRequest, bool>
    {
        private readonly ISizeModelTypeRepository sizeModelTypeRepository;
        private readonly ILogger<ChangeCodeSizeModelTypeItemRequestHandler> logger;

        public ChangeCodeSizeModelTypeItemRequestHandler(ISizeModelTypeRepository sizeModelTypeRepository,
                                                         ILogger<ChangeCodeSizeModelTypeItemRequestHandler> logger)
        {
            this.sizeModelTypeRepository = sizeModelTypeRepository ?? throw new ArgumentNullException(nameof(sizeModelTypeRepository));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<bool> Handle(ChangeCodeSizeModelTypeItemRequest request, CancellationToken cancellationToken)
        {
            var sizeModelTypeItem = await sizeModelTypeRepository.GetItemById(request.SizeModelTypeItemId);

            sizeModelTypeItem.ChangeCode(SizeModelTypeItemCode.Create(request.Code));

            if (!sizeModelTypeItem.IsValid())
            {
                logger.LogError($"Invalid size model type item. Errors: {sizeModelTypeItem.GetBrokenRules().ToString()}");
                return false;
            }

            sizeModelTypeRepository.ChangeItemCode(request.SizeModelTypeItemId, request.Code);

            await sizeModelTypeRepository.UnitOfWork.SaveEntitiesAsync(sizeModelTypeItem, cancellationToken);

            return true;
        }
    }
}
