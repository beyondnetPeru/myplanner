using MyPlanner.Plannings.Domain.PlanAggregate;
using MyPlanner.Plannings.Domain.SizeModels;
using MyPlanner.Plannings.Domain.SizeModelTypes;
using MyPlanner.Plannings.Shared.Domain.ValueObjects;

namespace MyPlanner.Plannings.Api.UseCases.Plan.Command.AddPlanItem
{
    public class AddPlanItemRequestHandler : IRequestHandler<AddPlanItemRequest, bool>
    {
        private readonly IPlanRepository planRepository;
        private readonly ISizeModelTypeRepository sizeModelTypeRepository;
        private readonly ILogger<AddPlanItemRequestHandler> logger;

        public AddPlanItemRequestHandler(IPlanRepository planRepository, ISizeModelTypeRepository sizeModelTypeRepository, ILogger<AddPlanItemRequestHandler> logger)
        {
            this.planRepository = planRepository ?? throw new ArgumentNullException(nameof(planRepository));
            this.sizeModelTypeRepository = sizeModelTypeRepository ?? throw new ArgumentNullException(nameof(sizeModelTypeRepository));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<bool> Handle(AddPlanItemRequest request, CancellationToken cancellationToken)
        {
            var sizeModelTypeFactor = await sizeModelTypeRepository.GetItemById(request.SizeModelTypeFactorId);

            var planItem = PlanItem.Create(IdValueObject.Create(),
                            sizeModelTypeFactor,
                            SizeModelTypeValueSelected.Create(request.SizeModelTypeValueSelected),
                            BusinessFeature.Create(request.CategoryName, request.Name, request.BusinessDefinition, request.ComplexityLevel, request.BacklogName, request.Priority, request.MoScoW),
                            TechnicalDefinition.Create(request.TechnicalDefinition),
                            ComponentsImpacted.Create(request.ComponentsImpacted),
                            TechnicalDependencies.Create(request.TechnicalDependencies),
                            BallParkCost.Create(request.BallParkCost),
                            BallParkDependenciesCost.Create(request.BallParkDependenciesCost),
                            BallParkTotalCost.Create(0.00),
                            KeyAssumptions.Create(request.KeyAssumptions),
                            UserId.Create(request.UserId));


            if (!planItem.IsValid())
            {
                logger.LogError($"PlanItem is not valid. Errros:{planItem.GetBrokenRules()}");
                return false;
            }

            var plan = await planRepository.GetByIdAsync(request.PlanId);

            plan.AddPlanItem(planItem, UserId.Create(request.UserId));

            if (!plan.IsValid())
            {
                logger.LogError($"Plan is not valid. Errros:{plan.GetBrokenRules()}");
                return false;
            }

            planRepository.AddItem(planItem);

            await planRepository.UnitOfWork.SaveEntitiesAsync(planItem, cancellationToken);

            return true;
        }
    }
}
