using MyPlanner.Plannings.Api.Endpoints;
using MyPlanner.Plannings.Api.Services.Impl;
using MyPlanner.Plannings.Api.Services.Interfaces;
using MyPlanner.Plannings.Domain.SizeModels;
using MyPlanner.Plannings.Shared.Domain.ValueObjects;

namespace MyPlanner.Plannings.Api.UseCases.SizeModelTypes.Commands.CreateSizeModelType
{
    public class CreateSizeModelTypeRequestHandler : IRequestHandler<CreateSizeModelTypeRequest, bool>
    {
        private readonly ISizeModelTypeRepository sizeModelTypeRepository;
        private readonly IPlanningIntegrationEventService planningIntegrationEventService;
        private readonly ILogger<CreateSizeModelTypeRequestHandler> logger;
        private readonly IMapper mapper;

        public CreateSizeModelTypeRequestHandler(ISizeModelTypeRepository sizeModelTypeRepository,
                                                 IPlanningIntegrationEventService planningIntegrationEventService,
                                                 ILogger<CreateSizeModelTypeRequestHandler> logger,
                                                 IMapper mapper)
        {
            this.sizeModelTypeRepository = sizeModelTypeRepository ?? throw new ArgumentNullException(nameof(sizeModelTypeRepository));
            this.planningIntegrationEventService = planningIntegrationEventService ?? throw new ArgumentNullException(nameof(planningIntegrationEventService));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<bool> Handle(CreateSizeModelTypeRequest request, CancellationToken cancellationToken)
        {

            var planningIntegrationEvent = new PlanningIntegrationEvent(request.UserId);
            await planningIntegrationEventService.AddAndSaveEventAsync(planningIntegrationEvent);


            var props = mapper.Map<SizeModelTypeProps>(request);
            var sizeModelType = SizeModelType.Create(IdValueObject.Create(), props.Code, props.Name);

            if (request.Items != null)
            {
                foreach (var item in request.Items)
                {
                    var factor = SizeModelTypeItem.Create(IdValueObject.Create(),
                                                            SizeModelTypeItemCode.Create(item.Code),
                                                            Name.Create(item.Name),
                                                            sizeModelType);

                    if (!factor.IsValid())
                    {
                        logger.LogError($"SizeModelTypeFactor is not valid. Errors: {factor.GetBrokenRules().ToString()}");
                        return false;
                    }

                    sizeModelType.AddItem(factor);
                }
            }


            if (!sizeModelType.IsValid())
            {
                logger.LogError($"SizeModelType is not valid. Errors: {sizeModelType.GetBrokenRules().ToString()}");
                return false;
            }

            sizeModelTypeRepository.Add(sizeModelType);

            await sizeModelTypeRepository.UnitOfWork.SaveEntitiesAsync(sizeModelType, cancellationToken);

            return true;

        }
    }
}
