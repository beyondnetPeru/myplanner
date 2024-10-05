using MyPlanner.Plannings.Domain.SizeModelTypes;
using MyPlanner.Shared.Cqrs;

namespace MyPlanner.Plannings.Api.UseCases.SizeModelTypes.Commands.ChangeCodeSizeModelType
{
    public class ChangeCodeSizeModelTypeRequestHandler : AbstractCommandHandler<ChangeCodeSizeModelTypeRequest, ResultSet>
    {
        private readonly ISizeModelTypeRepository sizeModelTypeRepository;
        
        public ChangeCodeSizeModelTypeRequestHandler(ISizeModelTypeRepository sizeModelTypeRepository, ILogger<ChangeCodeSizeModelTypeRequestHandler> logger): base(logger)
        {
            this.sizeModelTypeRepository = sizeModelTypeRepository ?? throw new ArgumentNullException(nameof(sizeModelTypeRepository));
        }
        
        public override async Task<ResultSet> HandleCommand(ChangeCodeSizeModelTypeRequest request, CancellationToken cancellationToken)
        {
            var entity = await sizeModelTypeRepository.GetById(request.SizeModelTypeId);

            entity.ChangeCode(SizeModelTypeCode.Create(request.Code));

            if (!entity.IsValid())
            {
                return ResultSet.Error($"SizeModelType code change has errors:{entity.GetBrokenRules()}");
            }

            sizeModelTypeRepository.ChangeCode(request.SizeModelTypeId, entity.GetPropsCopy().Code.GetValue());

            await sizeModelTypeRepository.UnitOfWork.SaveEntitiesAsync(entity, cancellationToken);

            return ResultSet.Success();
        }
    }
}
