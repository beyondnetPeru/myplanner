using AutoMapper;
using MediatR;
using MyPlanner.Plannings.Api.Endpoints;
using BeyondNet.Ddd.ValueObjects;
using MyPlanner.Plannings.Domain.SizeModels;
using MyPlanner.Plannings.Shared.Domain.ValueObjects;

namespace MyPlanner.Plannings.Api.UseCases.SizeModelTypes.CreateSizeModelType
{
    public class CreateSizeModelTypeRequestHandler : IRequestHandler<CreateSizeModelTypeRequest, bool>
    {
        private readonly ISizeModelTypeRepository sizeModelTypeRepository;
        private readonly ILogger<CreateSizeModelTypeRequestHandler> logger;

        public CreateSizeModelTypeRequestHandler(ISizeModelTypeRepository sizeModelTypeRepository, ILogger<CreateSizeModelTypeRequestHandler> logger)
        {
            this.sizeModelTypeRepository = sizeModelTypeRepository ?? throw new ArgumentNullException(nameof(sizeModelTypeRepository));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<bool> Handle(CreateSizeModelTypeRequest request, CancellationToken cancellationToken)
        {
            var sizeModelType = SizeModelType.Create(
                IdValueObject.Create(),
                SizeModelTypeCode.Create(request.Code),
                Name.Create(request.Name));

            if (request.Factors.Any())
            {
                foreach (var item in request.Factors)
                {
                    sizeModelType.AddFactor(SizeModelTypeFactor.Create(IdValueObject.Create(),
                                            SizeModelTypeFactorCode.Create(item.Code),
                                            Name.Create(item.Name), sizeModelType));

                    if (!sizeModelType.IsValid())
                    {
                        logger.LogError($"The ${nameof(request)} following errors were found: {sizeModelType.GetBrokenRules()}");
                        return false;
                    }
                }
            }

            await sizeModelTypeRepository.Add(sizeModelType);

            await sizeModelTypeRepository.UnitOfWork.SaveEntitiesAsync(sizeModelType, cancellationToken);

            return true;

        }
    }
}
