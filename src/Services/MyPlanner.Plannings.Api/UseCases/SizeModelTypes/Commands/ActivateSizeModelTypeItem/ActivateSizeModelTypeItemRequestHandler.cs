using MyPlanner.Plannings.Domain.SizeModelTypes;
using MyPlanner.Shared.Cqrs;

namespace MyPlanner.Plannings.Api.UseCases.SizeModelTypes.Commands.ActivateSizeModelTypeFactor
{
    public class ActivateSizeModelTypeItemRequestHandler : AbstractCommandHandler<ActivateSizeModelTypeItemRequest, ResultSet>
    {
        private readonly ISizeModelTypeRepository sizeModelTypeRepository;

        public ActivateSizeModelTypeItemRequestHandler(ISizeModelTypeRepository sizeModelTypeRepository, ILogger<ActivateSizeModelTypeItemRequestHandler> logger):base(logger)
        {
            this.sizeModelTypeRepository = sizeModelTypeRepository;
        }

        public override async Task<ResultSet> HandleCommand(ActivateSizeModelTypeItemRequest request, CancellationToken cancellationToken)
        {
            var entity = await sizeModelTypeRepository.GetItemById(request.SizeModelTypeItemId);

            entity.Activate();

            if (!entity.IsValid())
            {
                return ResultSet.Error($"Error activating SizeModelTypeItem: {entity.GetBrokenRules()}" );
            }

            sizeModelTypeRepository.Activate(request.SizeModelTypeItemId);

            await sizeModelTypeRepository.UnitOfWork.SaveEntitiesAsync(entity, cancellationToken);

            return ResultSet.Success();
        }
    }
}
