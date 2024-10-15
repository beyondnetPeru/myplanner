using MyPlanner.Plannings.Domain.PlanAggregate;
using MyPlanner.Shared.Domain.ValueObjects;
using MyPlanner.Shared.Infrastructure.Idempotency;

namespace MyPlanner.Plannings.Api.UseCases.Plan.Command.ClosePlan
{
    public class ClosePlanCommandHandler : AbstractCommandHandler<ClosePlanCommand, ResultSet>
    {
        private readonly IPlanRepository planRepository;

        public ClosePlanCommandHandler(IPlanRepository planRepository, ILogger<ClosePlanCommandHandler> logger):base(logger)
        {
            this.planRepository = planRepository ?? throw new ArgumentNullException(nameof(planRepository));
        }

        public override async Task<ResultSet> HandleCommand(ClosePlanCommand request, CancellationToken cancellationToken)
        {
            var plan = await planRepository.GetByIdAsync(request.PlanId);

            plan.Close(UserId.Create(request.UserId));

            if (!plan.IsValid())
            {
                return ResultSet.Error($"Plan is not valid. Errors: {plan.GetBrokenRules().ToString()}");
            }

            planRepository.Update(plan);

            await planRepository.UnitOfWork.SaveEntitiesAsync(plan, cancellationToken);

            return ResultSet.Success();
        }
    }

    public class ClosePlanIdentifiedRequestHandler : IdentifiedCommandHandler<ClosePlanCommand, ResultSet>
    {
        public ClosePlanIdentifiedRequestHandler(
            IMediator mediator,
            IRequestManager requestManager,
            ILogger<IdentifiedCommandHandler<ClosePlanCommand, ResultSet>> logger)
            : base(mediator, requestManager, logger)
        {
        }

        protected override ResultSet CreateResultForDuplicateRequest()
        {
            return ResultSet.Success(); // Ignore duplicate requests for processing order.
        }
    }
}
