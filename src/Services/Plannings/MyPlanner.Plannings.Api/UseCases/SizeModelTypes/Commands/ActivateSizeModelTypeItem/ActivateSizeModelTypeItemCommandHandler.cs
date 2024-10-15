using MyPlanner.Plannings.Domain.SizeModelTypes;


namespace MyPlanner.Plannings.Api.UseCases.SizeModelTypes.Commands.ActivateSizeModelTypeFactor
{
    public class ActivateSizeModelTypeItemCommandHandler : AbstractCommandHandler<ActivateSizeModelTypeItemCommand, ResultSet>
    {
        private readonly ISizeModelTypeRepository sizeModelTypeRepository;

        public ActivateSizeModelTypeItemCommandHandler(ISizeModelTypeRepository sizeModelTypeRepository, ILogger<ActivateSizeModelTypeItemCommandHandler> logger):base(logger)
        {
            this.sizeModelTypeRepository = sizeModelTypeRepository;
        }

        public override async Task<ResultSet> HandleCommand(ActivateSizeModelTypeItemCommand request, CancellationToken cancellationToken)
        {
            var entity = await sizeModelTypeRepository.GetItemById(request.SizeModelTypeItemId);

            entity.Activate();

            if (!entity.IsValid())
            {
                return ResultSet.Error($"Error activating SizeModelTypeItem: {entity.GetBrokenRules()}" );
            }

            sizeModelTypeRepository.UpdateItem(entity);

            await sizeModelTypeRepository.UnitOfWork.SaveEntitiesAsync(entity, cancellationToken);

            return ResultSet.Success();
        }
    }
}
