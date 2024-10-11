using MyPlanner.Plannings.Domain.PlanAggregate;
using MyPlanner.Shared.Cqrs;
using MyPlanner.Shared.Domain.ValueObjects;
using MyPlanner.Shared.Infrastructure.Idempotency;

namespace MyPlanner.Plannings.Api.UseCases.Plan.Command.ChangeOwner
{
    public class ChangePlanOwnerCommandHandler : AbstractCommandHandler<ChangePlanOwnerCommand, ResultSet>
    {
        private readonly IPlanRepository planRepository;

        public ChangePlanOwnerCommandHandler(IPlanRepository planRepository, ILogger<ChangePlanOwnerCommandHandler> logger):base(logger)
        {
            this.planRepository = planRepository ?? throw new ArgumentNullException(nameof(planRepository));
                }

        public override async Task<ResultSet> HandleCommand(ChangePlanOwnerCommand request, CancellationToken cancellationToken)
        {
            var plan = await planRepository.GetByIdAsync(request.PlanId);

            plan.ChangeOwner(Owner.Create(request.Owner), UserId.Create(request.UserId));

            if (!plan.IsValid())
            {
                ResultSet.Error($"Plan is not valid. Errors:{plan.GetBrokenRules().ToString()}");
            }

            planRepository.Update(plan);

            await planRepository.UnitOfWork.SaveEntitiesAsync(plan, cancellationToken);

            return ResultSet.Success();
        }
    }

    public class ChangePlanOwnerIdentifiedRequestHandler : IdentifiedCommandHandler<ChangePlanOwnerCommand, ResultSet>
    {
        public ChangePlanOwnerIdentifiedRequestHandler(
            IMediator mediator,
            IRequestManager requestManager,
            ILogger<IdentifiedCommandHandler<ChangePlanOwnerCommand, ResultSet>> logger)
            : base(mediator, requestManager, logger)
        {
        }

        protected override ResultSet CreateResultForDuplicateRequest()
        {
            return ResultSet.Success(); // Ignore duplicate requests for processing order.
        }
    }
}
