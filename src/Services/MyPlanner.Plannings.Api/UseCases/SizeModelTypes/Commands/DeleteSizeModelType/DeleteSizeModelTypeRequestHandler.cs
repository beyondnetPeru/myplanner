using MyPlanner.Plannings.Domain.SizeModelTypes;
using MyPlanner.Shared.Cqrs;

namespace MyPlanner.Plannings.Api.UseCases.SizeModelTypes.Commands.DeleteSizeModelType
{
    public class DeleteSizeModelTypeRequestHandler : AbstractCommandHandler<DeleteSizeModelTypeRequest, ResultSet>
    {
        private readonly ISizeModelTypeRepository sizeModelTypeRepository;

        public DeleteSizeModelTypeRequestHandler(ISizeModelTypeRepository sizeModelTypeRepository, ILogger<DeleteSizeModelTypeRequestHandler> logger) : base(logger)
        {
            this.sizeModelTypeRepository = sizeModelTypeRepository ?? throw new ArgumentNullException(nameof(sizeModelTypeRepository));
        }

        public override async Task<ResultSet> HandleCommand(DeleteSizeModelTypeRequest request, CancellationToken cancellationToken)
        {
            var sizeModelType = await sizeModelTypeRepository.GetById(request.SizeModelTypeId);

            sizeModelType.Delete();

            if (!sizeModelType.IsValid())
            {
                return ResultSet.Error($"SizeModelType cannot be deleted. Errors: {sizeModelType.GetBrokenRules().ToString()}");
            }

            sizeModelTypeRepository.Delete(request.SizeModelTypeId);

            await sizeModelTypeRepository.UnitOfWork.SaveEntitiesAsync(sizeModelType, cancellationToken);

            return ResultSet.Success();
        }
    }
}
