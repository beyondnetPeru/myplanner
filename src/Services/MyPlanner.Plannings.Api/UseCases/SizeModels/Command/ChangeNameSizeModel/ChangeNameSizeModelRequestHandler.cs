using MyPlanner.Plannings.Domain.SizeModels;
using MyPlanner.Shared.Cqrs;
using MyPlanner.Shared.Domain.ValueObjects;

namespace MyPlanner.Plannings.Api.UseCases.SizeModels.Command.ChangeSizeModelName
{
    public class ChangeNameSizeModelRequestHandler : AbstractCommandHandler<ChangeNameSizeModelRequest, ResultSet>
    {
        private readonly ISizeModelRepository sizeModelRepository;

        public ChangeNameSizeModelRequestHandler(ISizeModelRepository sizeModelRepository, ILogger<ChangeNameSizeModelRequestHandler> logger):base(logger)
        {
            this.sizeModelRepository = sizeModelRepository ?? throw new ArgumentNullException(nameof(sizeModelRepository));
        }

        public override async Task<ResultSet> HandleCommand(ChangeNameSizeModelRequest request, CancellationToken cancellationToken)
        {
            var sizeModel = await sizeModelRepository.Get(request.SizeModelId);

            sizeModel.ChangeName(Name.Create(request.Name), UserId.Create(request.UserId));

            if (!sizeModel.IsValid())
            {
                return ResultSet.Error($"SizeModel name cannot be changed. Errors:{sizeModel.GetBrokenRules()}");
            }

            sizeModelRepository.ChangeName(request.SizeModelId, request.Name);

            await sizeModelRepository.UnitOfWork.SaveEntitiesAsync(sizeModel, cancellationToken);

            return ResultSet.Success();
        }
    }
}
