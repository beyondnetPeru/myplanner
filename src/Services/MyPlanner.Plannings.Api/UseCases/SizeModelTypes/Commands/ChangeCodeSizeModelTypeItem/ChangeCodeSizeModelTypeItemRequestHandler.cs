using MyPlanner.Plannings.Domain.SizeModelTypes;
using MyPlanner.Shared.Cqrs;

namespace MyPlanner.Plannings.Api.UseCases.SizeModelTypes.Commands.ChangeCodeSizeModelTypeItem
{
    public class ChangeCodeSizeModelTypeItemRequestHandler : AbstractCommandHandler<ChangeCodeSizeModelTypeItemRequest, ResultSet>
    {
        private readonly ISizeModelTypeRepository sizeModelTypeRepository;

        public ChangeCodeSizeModelTypeItemRequestHandler(ISizeModelTypeRepository sizeModelTypeRepository,
                                                         ILogger<ChangeCodeSizeModelTypeItemRequestHandler> logger) : base(logger)
        {
            this.sizeModelTypeRepository = sizeModelTypeRepository ?? throw new ArgumentNullException(nameof(sizeModelTypeRepository));
        }

        public override async Task<ResultSet> HandleCommand(ChangeCodeSizeModelTypeItemRequest request, CancellationToken cancellationToken)
        {
            var sizeModelTypeItem = await sizeModelTypeRepository.GetItemById(request.SizeModelTypeItemId);

            sizeModelTypeItem.ChangeCode(SizeModelTypeItemCode.Create(request.Code));

            if (!sizeModelTypeItem.IsValid())
            {
                return ResultSet.Error($"Invalid size model type item. Errors: {sizeModelTypeItem.GetBrokenRules().ToString()}");
            }

            sizeModelTypeRepository.ChangeItemCode(request.SizeModelTypeItemId, request.Code);

            await sizeModelTypeRepository.UnitOfWork.SaveEntitiesAsync(sizeModelTypeItem, cancellationToken);

            return ResultSet.Success();
        }
    }
}
