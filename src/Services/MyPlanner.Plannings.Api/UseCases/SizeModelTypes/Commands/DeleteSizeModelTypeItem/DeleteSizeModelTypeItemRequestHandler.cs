using MyPlanner.Plannings.Domain.SizeModelTypes;

namespace MyPlanner.Plannings.Api.UseCases.SizeModelTypes.Commands.DeleteSizeModelTypeItem
{
    public class DeleteSizeModelTypeItemRequestHandler : IRequestHandler<DeleteSizeModelTypeItemRequest, bool>
    {
        private readonly ISizeModelTypeRepository sizeModelTypeRepository;
        private readonly ILogger<DeleteSizeModelTypeItemRequestHandler> logger;

        public DeleteSizeModelTypeItemRequestHandler(ISizeModelTypeRepository sizeModelTypeRepository,
                                                     ILogger<DeleteSizeModelTypeItemRequestHandler> logger)
        {
            this.sizeModelTypeRepository = sizeModelTypeRepository ?? throw new ArgumentNullException(nameof(sizeModelTypeRepository));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<bool> Handle(DeleteSizeModelTypeItemRequest request, CancellationToken cancellationToken)
        {
            var sizeModelTypeItem = await sizeModelTypeRepository.GetItemById(request.SizeModelTypeItemId);

            sizeModelTypeItem.Delete();

            if (!sizeModelTypeItem.IsValid())
            {
                logger.LogError($"Error deleting size model type item: {sizeModelTypeItem.GetBrokenRules().ToString()}");
                return false;
            }

            sizeModelTypeRepository.DeleteItem(request.SizeModelTypeItemId);

            await sizeModelTypeRepository.UnitOfWork.SaveEntitiesAsync(sizeModelTypeItem, cancellationToken);

            return true;
        }
    }
}
