using MyPlanner.Plannings.Domain.PlanAggregate;
using MyPlanner.Shared.Domain.ValueObjects;
using MyPlanner.Shared.Infrastructure.Idempotency;

namespace MyPlanner.Plannings.Api.UseCases.Plan.Command.ActivatePlan
{
    public class ActivatePlanCommandHandler : AbstractCommandHandler<ActivatePlanCommand, ResultSet>
    {
        private readonly IPlanRepository planRepository;

        public ActivatePlanCommandHandler(IPlanRepository planRepository, ILogger<ActivatePlanCommandHandler> logger):base(logger)
        {
            this.planRepository = planRepository ?? throw new ArgumentNullException(nameof(planRepository));
        }

        public override async Task<ResultSet> HandleCommand(ActivatePlanCommand request, CancellationToken cancellationToken)
        {
            var plan = await planRepository.GetByIdAsync(request.PlanId);

            if (!plan.IsValid())
            {
                return ResultSet.Error($"Plan is not valid. Errors:{plan.GetBrokenRules().ToString()}");
            }
            
            plan.Activate(UserId.Create(request.UserId));

            planRepository.Update(plan);

            await planRepository.UnitOfWork.SaveEntitiesAsync(plan, cancellationToken);

            return ResultSet.Success();
        }
    }

    public class ActivatePlanIdentifiedRequestHandler : IdentifiedCommandHandler<ActivatePlanCommand, ResultSet>
    {
        public ActivatePlanIdentifiedRequestHandler(
            IMediator mediator,
            IRequestManager requestManager,
            ILogger<IdentifiedCommandHandler<ActivatePlanCommand, ResultSet>> logger)
            : base(mediator, requestManager, logger)
        {
        }

        protected override ResultSet CreateResultForDuplicateRequest()
        {
            return ResultSet.Success(); // Ignore duplicate requests for processing order.
        }
    }
}
