using MyPlanner.Plannings.Domain.SizeModelTypes;


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
            var entity = await sizeModelTypeRepository.GetItemById(request.SizeModelTypeItemId);

            entity.Props.Name.SetValue(request.Name);

            if (!entity.IsValid())
            {
                return ResultSet.Error($"Invalid SizeModelTypeItem: Errors {entity.GetBrokenRules().ToString()}");
            }

            sizeModelTypeRepository.UpdateItem(entity);

            await sizeModelTypeRepository.UnitOfWork.SaveEntitiesAsync(entity, cancellationToken);

            return ResultSet.Success();

        }
    }
}
