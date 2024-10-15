using MyPlanner.Plannings.Domain.SizeModelTypes;


namespace MyPlanner.Plannings.Api.UseCases.SizeModelTypes.Commands.ChangeCodeSizeModelTypeItem
{
    public class ChangeCodeSizeModelTypeItemCommandHandler : AbstractCommandHandler<ChangeCodeSizeModelTypeItemCommand, ResultSet>
    {
        private readonly ISizeModelTypeRepository sizeModelTypeRepository;

        public ChangeCodeSizeModelTypeItemCommandHandler(ISizeModelTypeRepository sizeModelTypeRepository,
                                                         ILogger<ChangeCodeSizeModelTypeItemCommandHandler> logger) : base(logger)
        {
            this.sizeModelTypeRepository = sizeModelTypeRepository ?? throw new ArgumentNullException(nameof(sizeModelTypeRepository));
        }

        public override async Task<ResultSet> HandleCommand(ChangeCodeSizeModelTypeItemCommand request, CancellationToken cancellationToken)
        {
            var entity = await sizeModelTypeRepository.GetItemById(request.SizeModelTypeItemId);

            entity.ChangeCode(SizeModelTypeItemCode.Create(request.Code));

            if (!entity.IsValid())
            {
                return ResultSet.Error($"Invalid size model type item. Errors: {entity.GetBrokenRules().ToString()}");
            }

            sizeModelTypeRepository.UpdateItem(entity);

            await sizeModelTypeRepository.UnitOfWork.SaveEntitiesAsync(entity, cancellationToken);

            return ResultSet.Success();
        }
    }
}
