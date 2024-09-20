using MyPlanner.Plannings.Api.Endpoints;

namespace MyPlanner.Plannings.Api.UseCases.SizeModelTypes.Commands.DeleteSizeModelType
{
    public class DeleteSizeModelTypeRequestHandler : IRequestHandler<DeleteSizeModelTypeRequest, bool>
    {
        private readonly ISizeModelTypeRepository sizeModelTypeRepository;
        private readonly ILogger<DeleteSizeModelTypeRequestHandler> logger;

        public DeleteSizeModelTypeRequestHandler(ISizeModelTypeRepository sizeModelTypeRepository, ILogger<DeleteSizeModelTypeRequestHandler> logger)
        {
            this.sizeModelTypeRepository = sizeModelTypeRepository ?? throw new ArgumentNullException(nameof(sizeModelTypeRepository));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<bool> Handle(DeleteSizeModelTypeRequest request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var sizeModelType = await sizeModelTypeRepository.GetById(request.SizeModelTypeId);

            sizeModelType.Delete();

            if (!sizeModelType.IsValid())
            {
                logger.LogError($"SizeModelType cannot be deleted. Errors: {sizeModelType.GetBrokenRules().ToString()}");
                return false;
            }

            sizeModelTypeRepository.Delete(request.SizeModelTypeId);

            await sizeModelTypeRepository.UnitOfWork.SaveEntitiesAsync(sizeModelType, cancellationToken);

            return true;
        }
    }
}
