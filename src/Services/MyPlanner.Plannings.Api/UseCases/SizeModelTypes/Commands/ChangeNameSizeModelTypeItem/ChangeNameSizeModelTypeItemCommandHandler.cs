using MyPlanner.Plannings.Domain.SizeModelTypes;
using MyPlanner.Shared.Cqrs;

namespace MyPlanner.Plannings.Api.UseCases.SizeModelTypes.Commands.ChangeNameSizeModelTypeItem
{
    public class ChangeNameSizeModelTypeItemCommandHandler : AbstractCommandHandler<ChangeNameSizeModelTypeItemCommand, ResultSet>
    {
        private readonly ISizeModelTypeRepository sizeModelTypeRepository;

        public ChangeNameSizeModelTypeItemCommandHandler(ISizeModelTypeRepository sizeModelTypeRepository,
                                                         ILogger<ChangeNameSizeModelTypeItemCommandHandler> logger) : base(logger)
        {
            this.sizeModelTypeRepository = sizeModelTypeRepository ?? throw new ArgumentNullException(nameof(sizeModelTypeRepository));
        }

        public override async Task<ResultSet> HandleCommand(ChangeNameSizeModelTypeItemCommand request, CancellationToken cancellationToken)
        {
            var sizeModelTypeItem = await sizeModelTypeRepository.GetItemById(request.SizeModelTypeItemId);

            sizeModelTypeItem.GetProps().Name.SetValue(request.Name);

            if (!sizeModelTypeItem.IsValid())
            {
                return ResultSet.Error($"Invalid SizeModelTypeItem: Errors {sizeModelTypeItem.GetBrokenRules().ToString()}");
            }

            sizeModelTypeRepository.ChangeItemName(request.SizeModelTypeItemId, request.Name);

            await sizeModelTypeRepository.UnitOfWork.SaveEntitiesAsync(sizeModelTypeItem, cancellationToken);

            return ResultSet.Success();

        }
    }
}
