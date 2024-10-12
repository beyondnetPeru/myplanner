using MyPlanner.Plannings.Domain.SizeModels;

using MyPlanner.Shared.Domain.ValueObjects;

namespace MyPlanner.Plannings.Api.UseCases.SizeModels.Command.ActivateSizeModelItem
{
    public class ActivateSizeModelItemCommandHandler : AbstractCommandHandler<ActivateSizeModelItemCommand, ResultSet>
    {
        private readonly ISizeModelRepository sizeModelRepository;

        public ActivateSizeModelItemCommandHandler(ISizeModelRepository sizeModelRepository, ILogger<ActivateSizeModelItemCommandHandler> logger):base(logger) 
        {
            this.sizeModelRepository = sizeModelRepository ?? throw new ArgumentNullException(nameof(sizeModelRepository));
        }

        public override async Task<ResultSet> HandleCommand(ActivateSizeModelItemCommand request, CancellationToken cancellationToken)
        {
            var sizeModelItem = await sizeModelRepository.GetItem(request.SizeModelItemId);

            sizeModelItem.Activate(UserId.Create(request.UserId));

            if (!sizeModelItem.IsValid())
            {
                return ResultSet.Error($"SizeModelItem cannot be activated. Errors: {sizeModelItem.GetBrokenRules()}");
            }

            sizeModelRepository.UpdateItem(sizeModelItem);

            await sizeModelRepository.UnitOfWork.SaveEntitiesAsync(sizeModelItem, cancellationToken);

            return ResultSet.Success("SizeModel Item activated successfully.");
        }
    }
}
