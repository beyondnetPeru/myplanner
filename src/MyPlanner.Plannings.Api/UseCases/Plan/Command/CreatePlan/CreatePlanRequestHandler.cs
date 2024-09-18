using MyPlanner.Plannings.Domain.PlanAggregate;
using MyPlanner.Plannings.Shared.Domain.ValueObjects;

namespace MyPlanner.Plannings.Api.UseCases.Plan.Command.CreatePlan
{
    public class CreatePlanRequestHandler : IRequestHandler<CreatePlanRequest, bool>
    {
        private readonly IPlanRepository planRepository;
        private readonly ILogger<CreatePlanRequestHandler> logger;

        public CreatePlanRequestHandler(IPlanRepository planRepository, ILogger<CreatePlanRequestHandler> logger)
        {
            this.planRepository = planRepository ?? throw new ArgumentNullException(nameof(planRepository));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<bool> Handle(CreatePlanRequest request, CancellationToken cancellationToken)
        {
            var plan = Domain.PlanAggregate.Plan.Create(IdValueObject.Create(),
                                                        IdValueObject.Create(request.SizeModelTypeId),
                                                        Name.Create(request.SizeModelTypeName),
                                                        Name.Create(request.Name),
                                                        Owner.Create(request.Owner),
                                                        UserId.Create(request.UserId));

            if (!plan.IsValid())
            {
                logger.LogError($"Invalid plan. Errors: {plan.GetBrokenRules()}");
                return false;
            }

            planRepository.Create(plan);

            await planRepository.UnitOfWork.SaveEntitiesAsync(this, cancellationToken);

            return true;
        }
    }
}
