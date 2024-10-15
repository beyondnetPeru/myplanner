using MyPlanner.Plannings.Domain.SizeModelTypes;


namespace MyPlanner.Plannings.Api.UseCases.SizeModelTypes.Commands.ChangeCodeSizeModelType
{
    public class ChangeCodeSizeModelTypeCommandHandler : AbstractCommandHandler<ChangeCodeSizeModelTypeCommand, ResultSet>
    {
        private readonly ISizeModelTypeRepository sizeModelTypeRepository;
        
        public ChangeCodeSizeModelTypeCommandHandler(ISizeModelTypeRepository sizeModelTypeRepository, ILogger<ChangeCodeSizeModelTypeCommandHandler> logger): base(logger)
        {
            this.sizeModelTypeRepository = sizeModelTypeRepository ?? throw new ArgumentNullException(nameof(sizeModelTypeRepository));
        }
        
        public override async Task<ResultSet> HandleCommand(ChangeCodeSizeModelTypeCommand request, CancellationToken cancellationToken)
        {
            var entity = await sizeModelTypeRepository.GetById(request.SizeModelTypeId);

            entity.ChangeCode(SizeModelTypeCode.Create(request.Code));

            if (!entity.IsValid())
            {
                return ResultSet.Error($"SizeModelType code change has errors:{entity.GetBrokenRules()}");
            }

            sizeModelTypeRepository.Update(entity);

            await sizeModelTypeRepository.UnitOfWork.SaveEntitiesAsync(entity, cancellationToken);

            return ResultSet.Success();
        }
    }
}
