using MyPlanner.Plannings.Domain.SizeModelTypes;
using MyPlanner.Shared.Cqrs;

namespace MyPlanner.Plannings.Api.UseCases.SizeModelTypes.Commands.ActivateSizeModelType
{
    public class ActivateSizeModelTypeRequestHandler : AbstractCommandHandler<ActivateSizeModelTypeRequest, ResultSet>
    {
        private readonly ISizeModelTypeRepository sizeModelTypeRepository;

        public ActivateSizeModelTypeRequestHandler(ISizeModelTypeRepository sizeModelTypeRepository,
                                                   ILogger<ActivateSizeModelTypeRequestHandler> logger) : base(logger)
        {
            this.sizeModelTypeRepository = sizeModelTypeRepository ?? throw new ArgumentNullException(nameof(sizeModelTypeRepository));
        }

        public override async Task<ResultSet> HandleCommand (ActivateSizeModelTypeRequest request, CancellationToken cancellationToken)
        {
            var entity = await sizeModelTypeRepository.GetById(request.SizeModelTypeId);

            entity.Activate();

            if (!entity.IsValid())
            {
                return ResultSet.Error($"SizeModelType code change has errors:{entity.GetBrokenRules()}");                
            }

            sizeModelTypeRepository.Activate(request.SizeModelTypeId);

            await sizeModelTypeRepository.UnitOfWork.SaveEntitiesAsync(entity, cancellationToken);

            return ResultSet.Success();
        }
    }
}
