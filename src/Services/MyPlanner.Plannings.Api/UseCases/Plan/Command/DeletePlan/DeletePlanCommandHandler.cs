using MyPlanner.Plannings.Domain.PlanAggregate;
using MyPlanner.Shared.Cqrs;
using MyPlanner.Shared.Domain.ValueObjects;
using MyPlanner.Shared.Infrastructure.Idempotency;

namespace MyPlanner.Plannings.Api.UseCases.Plan.Command.DeletePlan
{
    public class DeletePlanCommandHandler : AbstractCommandHandler<DeletePlanCommand, ResultSet>
    {
        private readonly IPlanRepository planRepository;

        public DeletePlanCommandHandler(IPlanRepository planRepository, ILogger<DeletePlanCommandHandler> logger): base(logger)
        {
            this.planRepository = planRepository ?? throw new ArgumentNullException(nameof(planRepository));
        }

        public override async Task<ResultSet> HandleCommand(DeletePlanCommand request, CancellationToken cancellationToken)
        {
            var plan = await planRepository.GetByIdAsync(request.PlanId);

            plan.Delete(request.PlanId, UserId.Create(request.UserId));

            if (!plan.IsValid())
            {
                return ResultSet.Error($"Plan is not valid. Errors:{plan.GetBrokenRules().ToString()}");
            }

            await planRepository.UnitOfWork.SaveEntitiesAsync(plan, cancellationToken);

            return ResultSet.Success();
        }
    }

    public class DeletePlanIdentifiedRequestHandler : IdentifiedCommandHandler<DeletePlanCommand, ResultSet>
    {
        public DeletePlanIdentifiedRequestHandler(
            IMediator mediator,
            IRequestManager requestManager,
            ILogger<IdentifiedCommandHandler<DeletePlanCommand, ResultSet>> logger)
            : base(mediator, requestManager, logger)
        {
        }

        protected override ResultSet CreateResultForDuplicateRequest()
        {
            return ResultSet.Success(); // Ignore duplicate requests for processing order.
        }
    }
}
