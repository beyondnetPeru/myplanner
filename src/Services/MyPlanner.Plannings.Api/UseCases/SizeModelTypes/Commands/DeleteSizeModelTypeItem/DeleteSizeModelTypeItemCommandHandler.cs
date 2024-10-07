using MyPlanner.Plannings.Domain.SizeModelTypes;
using MyPlanner.Shared.Cqrs;

namespace MyPlanner.Plannings.Api.UseCases.SizeModelTypes.Commands.DeleteSizeModelTypeItem
{
    public class DeleteSizeModelTypeItemCommandHandler : AbstractCommandHandler<DeleteSizeModelTypeItemCommand, ResultSet>
    {
        private readonly ISizeModelTypeRepository sizeModelTypeRepository;

        public DeleteSizeModelTypeItemCommandHandler(ISizeModelTypeRepository sizeModelTypeRepository,
                                                     ILogger<DeleteSizeModelTypeItemCommandHandler> logger) : base(logger)
        {
            this.sizeModelTypeRepository = sizeModelTypeRepository ?? throw new ArgumentNullException(nameof(sizeModelTypeRepository));
        }

        public override async Task<ResultSet> HandleCommand(DeleteSizeModelTypeItemCommand request, CancellationToken cancellationToken)
        {
            var sizeModelTypeItem = await sizeModelTypeRepository.GetItemById(request.SizeModelTypeItemId);

            sizeModelTypeItem.Delete();

            if (!sizeModelTypeItem.IsValid())
            {
                return ResultSet.Error($"Error deleting size model type item: {sizeModelTypeItem.GetBrokenRules().ToString()}");
            }

            sizeModelTypeRepository.DeleteItem(request.SizeModelTypeItemId);

            await sizeModelTypeRepository.UnitOfWork.SaveEntitiesAsync(sizeModelTypeItem, cancellationToken);

            return ResultSet.Success();
        }
    }
}
