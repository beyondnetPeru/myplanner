using MyPlanner.Plannings.Domain.SizeModelTypes;
using MyPlanner.Shared.Cqrs;

namespace MyPlanner.Plannings.Api.UseCases.SizeModelTypes.Commands.DeactivateSizeModelType
{
    public class DeactivateSizeModelTypeCommandHandler : AbstractCommandHandler<DeactivateSizeModelTypeCommand, ResultSet>
    {
        private readonly ISizeModelTypeRepository sizeModelTypeRepository;

        public DeactivateSizeModelTypeCommandHandler(ISizeModelTypeRepository sizeModelTypeRepository,
                                                     ILogger<DeactivateSizeModelTypeCommandHandler> logger) : base(logger)
        {
            this.sizeModelTypeRepository = sizeModelTypeRepository ?? throw new ArgumentNullException(nameof(sizeModelTypeRepository));
        }

        public override async Task<ResultSet> HandleCommand(DeactivateSizeModelTypeCommand request, CancellationToken cancellationToken)
        {
            var entity = await sizeModelTypeRepository.GetById(request.SizeModelTypeId);

            entity.Deactivate();

            if (!entity.IsValid())
            {
                return ResultSet.Error($"SizeModelType code change has errors:{entity.GetBrokenRules()}");
            }

            sizeModelTypeRepository.Deactivate(request.SizeModelTypeId);

            await sizeModelTypeRepository.UnitOfWork.SaveEntitiesAsync(entity, cancellationToken);

            return ResultSet.Success();
        }
    }
}
