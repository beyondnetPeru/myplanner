using MyPlanner.Plannings.Domain.SizeModelTypes;
using MyPlanner.Shared.Cqrs;
using MyPlanner.Shared.Domain.ValueObjects;

namespace MyPlanner.Plannings.Api.UseCases.SizeModelTypes.Commands.ChangeNameSizeModelType
{
    public class ChangeNameSizeModelTypeCommandHandler : AbstractCommandHandler<ChangeNameSizeModelTypeCommand, ResultSet>
    {
        private readonly ISizeModelTypeRepository sizeModelTypeRepository;

        public ChangeNameSizeModelTypeCommandHandler(ISizeModelTypeRepository sizeModelTypeRepository,
                                                     ILogger<ChangeNameSizeModelTypeCommandHandler> logger) : base(logger)
        {
            this.sizeModelTypeRepository = sizeModelTypeRepository ?? throw new ArgumentNullException(nameof(sizeModelTypeRepository));
        }

        public override async Task<ResultSet> HandleCommand(ChangeNameSizeModelTypeCommand request, CancellationToken cancellationToken)
        {
            var entity = await sizeModelTypeRepository.GetById(request.SizeModelTypeId);

            entity.ChangeName(Name.Create(request.Name));

            if (!entity.IsValid())
            {
                return ResultSet.Error($"SizeModelType code change has errors:{entity.GetBrokenRules()}");
            }

            sizeModelTypeRepository.ChangeName(request.SizeModelTypeId, entity.GetPropsCopy().Name.GetValue());

            await sizeModelTypeRepository.UnitOfWork.SaveEntitiesAsync(entity, cancellationToken);

            return ResultSet.Success();
        }
    }
}
