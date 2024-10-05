using MyPlanner.Plannings.Domain.SizeModels;
using MyPlanner.Shared.Cqrs;
using MyPlanner.Shared.Domain.ValueObjects;

namespace MyPlanner.Plannings.Api.UseCases.SizeModels.Command.ActivateSizeModelItem
{
    public class ActivateSizeModelItemRequestHandler : AbstractCommandHandler<ActivateSizeModelItemRequest, ResultSet>
    {
        private readonly ISizeModelRepository sizeModelRepository;

        public ActivateSizeModelItemRequestHandler(ISizeModelRepository sizeModelRepository, ILogger<ActivateSizeModelItemRequestHandler> logger):base(logger) 
        {
            this.sizeModelRepository = sizeModelRepository ?? throw new ArgumentNullException(nameof(sizeModelRepository));
        }

        public override async Task<ResultSet> HandleCommand(ActivateSizeModelItemRequest request, CancellationToken cancellationToken)
        {
            var sizeModelItem = await sizeModelRepository.GetItem(request.SizeModelItemId);

            sizeModelItem.Activate(UserId.Create(request.UserId));

            if (!sizeModelItem.IsValid())
            {
                return ResultSet.Error($"SizeModelItem cannot be activated. Errors: {sizeModelItem.GetBrokenRules()}");
            }

            sizeModelRepository.ActiveItem(request.SizeModelItemId);

            await sizeModelRepository.UnitOfWork.SaveEntitiesAsync(sizeModelItem, cancellationToken);

            return ResultSet.Success("SizeModel Item activated successfully.");
        }
    }
}
