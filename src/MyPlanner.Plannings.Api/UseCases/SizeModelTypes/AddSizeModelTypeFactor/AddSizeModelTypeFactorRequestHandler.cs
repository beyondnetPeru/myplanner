using BeyondNet.Ddd.ValueObjects;
using MediatR;
using MyPlanner.Plannings.Api.Endpoints;
using MyPlanner.Plannings.Domain.SizeModels;
using MyPlanner.Plannings.Shared.Domain.ValueObjects;

namespace MyPlanner.Plannings.Api.UseCases.SizeModelTypes.AddSizeModelTypeFactor
{
    public class AddSizeModelTypeFactorRequestHandler : IRequestHandler<AddSizeModelTypeFactorRequest, bool>
    {
        private readonly ISizeModelTypeRepository sizeModelTypeRepository;
        private readonly ISizeModelTypeFactorRepository sizeModelTypeFactorRepository;
        private readonly ILogger<AddSizeModelTypeFactorRequestHandler> logger;

        public AddSizeModelTypeFactorRequestHandler(ISizeModelTypeRepository sizeModelTypeRepository,
                                                    ISizeModelTypeFactorRepository sizeModelTypeFactorRepository,
                                                    ILogger<AddSizeModelTypeFactorRequestHandler> logger)
        {
            this.sizeModelTypeRepository = sizeModelTypeRepository ?? throw new ArgumentNullException(nameof(sizeModelTypeRepository));
            this.sizeModelTypeFactorRepository = sizeModelTypeFactorRepository ?? throw new ArgumentNullException(nameof(sizeModelTypeFactorRepository));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<bool> Handle(AddSizeModelTypeFactorRequest request, CancellationToken cancellationToken)
        {
            var sizeModelType = await sizeModelTypeRepository.GetById(request.SizeModelId);

            var factor = SizeModelTypeFactor.Create(IdValueObject.Create(),
                                                    SizeModelTypeFactorCode.Create(request.Code),
                                                    Name.Create(request.Name), sizeModelType);

            sizeModelType.AddFactor(factor);

            if (!factor.IsValid())
            {
                logger.LogError($"Invalid factor. Error: {factor.GetBrokenRules()}");
                return false;
            }

            await sizeModelTypeFactorRepository.Add(factor);

            await sizeModelTypeRepository.UnitOfWork.SaveEntitiesAsync(this, cancellationToken);

            return true;
        }
    }
}
