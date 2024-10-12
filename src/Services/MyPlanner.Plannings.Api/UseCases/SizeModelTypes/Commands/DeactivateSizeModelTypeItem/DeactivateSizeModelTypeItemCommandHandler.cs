using MyPlanner.Plannings.Domain.SizeModelTypes;


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
            var entity = await sizeModelTypeRepository.GetItemById(request.SizeModelTypeItemId);

            entity.Deactivate();

            if (!entity.IsValid())
            {
                return ResultSet.Error($"Error deactivating size model type factor {request.SizeModelTypeItemId}. Errors:{entity.GetBrokenRules()}");
            }

            sizeModelTypeRepository.UpdateItem(entity);

            await sizeModelTypeRepository.UnitOfWork.SaveEntitiesAsync(entity, cancellationToken);

            return ResultSet.Success();
        }
    }
}
