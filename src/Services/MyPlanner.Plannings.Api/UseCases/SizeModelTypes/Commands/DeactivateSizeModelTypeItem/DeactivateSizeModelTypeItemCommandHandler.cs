using MyPlanner.Plannings.Domain.SizeModelTypes;
using MyPlanner.Shared.Cqrs;

namespace MyPlanner.Plannings.Api.UseCases.SizeModelTypes.Commands.DeactivateSizeModelTypeFactor
{
    public class DeactivateSizeModelTypeItemCommandHandler : AbstractCommandHandler<DeactivateSizeModelTypeItemCommand, ResultSet>
    {
        private readonly ISizeModelTypeRepository sizeModelTypeRepository;

        public DeactivateSizeModelTypeItemCommandHandler(ISizeModelTypeRepository sizeModelTypeRepository, ILogger<DeactivateSizeModelTypeItemCommandHandler> logger) : base(logger)
        {
            this.sizeModelTypeRepository = sizeModelTypeRepository;
        }

        public override async Task<ResultSet> HandleCommand(DeactivateSizeModelTypeItemCommand request, CancellationToken cancellationToken)
        {
            var sizeModelTypeItem = await sizeModelTypeRepository.GetItemById(request.SizeModelTypeItemId);

            sizeModelTypeItem.Deactivate();

            if (!sizeModelTypeItem.IsValid())
            {
                return ResultSet.Error($"Error deactivating size model type factor {request.SizeModelTypeItemId}. Errors:{sizeModelTypeItem.GetBrokenRules()}");
            }

            sizeModelTypeRepository.Deactivate(request.SizeModelTypeItemId);

            await sizeModelTypeRepository.UnitOfWork.SaveEntitiesAsync(sizeModelTypeItem, cancellationToken);

            return ResultSet.Success();
        }
    }
}
