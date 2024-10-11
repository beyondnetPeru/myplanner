using MyPlanner.Plannings.Domain.PlanAggregate;
using MyPlanner.Shared.Cqrs;
using MyPlanner.Shared.Domain.ValueObjects;
using MyPlanner.Shared.Infrastructure.Idempotency;

namespace MyPlanner.Plannings.Api.UseCases.Plan.Command.ChangeName
{
    public class ChangePlanNameCommandHandler : AbstractCommandHandler<ChangePlanNameCommand, ResultSet>
    {
        private readonly IPlanRepository planRepository;

        public ChangePlanNameCommandHandler(IPlanRepository planRepository, ILogger<ChangePlanNameCommandHandler> logger):base(logger)
        {
            this.planRepository = planRepository;
        }

        public override async Task<ResultSet> HandleCommand(ChangePlanNameCommand request, CancellationToken cancellationToken)
        {
            var plan = await planRepository.GetByIdAsync(request.PlanId);

            plan.ChangeName(Name.Create(request.Name), UserId.Create(request.UserId));

            if (!plan.IsValid())
            {
                return ResultSet.Error($"Name for plan wrong. Errors: {plan.GetBrokenRules().ToString()}");
            }

            planRepository.Update(plan);

            await planRepository.UnitOfWork.SaveEntitiesAsync(plan, cancellationToken);

            return ResultSet.Success();
        }
    }

    public class ChangePlanNameIdentifiedRequestHandler : IdentifiedCommandHandler<ChangePlanNameCommand, ResultSet>
    {
        public ChangePlanNameIdentifiedRequestHandler(
            IMediator mediator,
            IRequestManager requestManager,
            ILogger<IdentifiedCommandHandler<ChangePlanNameCommand, ResultSet>> logger)
            : base(mediator, requestManager, logger)
        {
        }

        protected override ResultSet CreateResultForDuplicateRequest()
        {
            return ResultSet.Success(); // Ignore duplicate requests for processing order.
        }
    }
}
