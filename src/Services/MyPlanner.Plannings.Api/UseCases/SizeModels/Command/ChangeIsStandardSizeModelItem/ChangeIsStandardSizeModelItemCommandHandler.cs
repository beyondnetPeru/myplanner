using MyPlanner.Plannings.Domain.SizeModels;
using MyPlanner.Shared.Cqrs;
using MyPlanner.Shared.Domain.ValueObjects;

namespace MyPlanner.Plannings.Api.UseCases.SizeModels.Command.ChangeIsStandardSizeModelItem
{
    public class ChangeIsStandardSizeModelItemCommandHandler : AbstractCommandHandler<ChangeIsStandardSizeModelItemCommand, ResultSet>
    {
        private readonly ISizeModelRepository sizeModelRepository;

        public ChangeIsStandardSizeModelItemCommandHandler(ISizeModelRepository sizeModelRepository, ILogger<ChangeIsStandardSizeModelItemCommandHandler> logger):base(logger)
        {
            this.sizeModelRepository = sizeModelRepository ?? throw new ArgumentNullException(nameof(sizeModelRepository));
        }

        public override async Task<ResultSet> HandleCommand(ChangeIsStandardSizeModelItemCommand request, CancellationToken cancellationToken)
        {
            var sizeModelItem = await sizeModelRepository.GetItem(request.SizeModelItemId);

            sizeModelItem.ChangeIsStandard(request.IsStandard, UserId.Create(request.UserId));

            if (!sizeModelItem.IsValid())
            {
                return ResultSet.Error($"Invalid size model item. Errors: {sizeModelItem.GetBrokenRules()}");
            }

            sizeModelRepository.UpdateItem(sizeModelItem);

            await sizeModelRepository.UnitOfWork.SaveEntitiesAsync(sizeModelItem, cancellationToken);

            return ResultSet.Success();
        }
    }
}
