using MyPlanner.Plannings.Api.Services.Impl;
using MyPlanner.Plannings.Api.Services.Interfaces;
using MyPlanner.Plannings.Domain.SizeModelTypes;
using MyPlanner.Shared.Cqrs;
using MyPlanner.Shared.Domain.ValueObjects;

namespace MyPlanner.Plannings.Api.UseCases.SizeModelTypes.Commands.CreateSizeModelType
{
    public class CreateSizeModelTypeCommandHandler : AbstractCommandHandler<CreateSizeModelTypeCommand, ResultSet>
    {
        private readonly ISizeModelTypeRepository sizeModelTypeRepository;
        private readonly IPlanningIntegrationEventService planningIntegrationEventService;
        private readonly IMapper mapper;

        public CreateSizeModelTypeCommandHandler(ISizeModelTypeRepository sizeModelTypeRepository,
                                                 IPlanningIntegrationEventService planningIntegrationEventService,
                                                 ILogger<CreateSizeModelTypeCommandHandler> logger,
                                                 IMapper mapper) : base(logger) 
        {
            this.sizeModelTypeRepository = sizeModelTypeRepository ?? throw new ArgumentNullException(nameof(sizeModelTypeRepository));
            this.planningIntegrationEventService = planningIntegrationEventService ?? throw new ArgumentNullException(nameof(planningIntegrationEventService));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public override async Task<ResultSet> HandleCommand(CreateSizeModelTypeCommand request, CancellationToken cancellationToken)
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
                        return ResultSet.Error($"SizeModelTypeFactor is not valid. Errors: {factor.GetBrokenRules().ToString()}");
                    }

                    sizeModelType.AddItem(factor);
                }
            }


            if (!sizeModelType.IsValid())
            {
                return ResultSet.Error($"SizeModelType is not valid. Errors: {sizeModelType.GetBrokenRules().ToString()}");
            }

            sizeModelTypeRepository.Add(sizeModelType);

            await sizeModelTypeRepository.UnitOfWork.SaveEntitiesAsync(sizeModelType, cancellationToken);

            return ResultSet.Success();

        }
    }
}
