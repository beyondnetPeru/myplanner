using MyPlanner.Plannings.Api.UseCases.Plan.Command.ChangeTechnicalDefinitionPlanItem;
using MyPlanner.Plannings.Domain.PlanAggregate;
using MyPlanner.Shared.Cqrs;
using MyPlanner.Shared.Domain.ValueObjects;

namespace MyPlanner.Plannings.Api.UseCases.Plan.Command.ChangeTechnicalDefinition
{
    public class ChangeTechnicalDefinitionCommandHandler : AbstractCommandHandler<ChangeTechnicalDefinitionCommand, ResultSet>
    {
        private readonly IPlanRepository planRepository;

        public ChangeTechnicalDefinitionCommandHandler(IPlanRepository planRepository, ILogger<AbstractCommandHandler<ChangeTechnicalDefinitionCommand, ResultSet>> Logger) : base(Logger)
        {
            this.planRepository = planRepository ?? throw new ArgumentNullException(nameof(planRepository));
        }

        public async override Task<ResultSet> HandleCommand(ChangeTechnicalDefinitionCommand request, CancellationToken cancellationToken)
        {
            var planItem = await planRepository.GetItemById(request.PlanItemId);

            planItem.ChangeTechnicalDefinition(TechnicalDefinition.Create(request.TechnicalDefinition), UserId.Create(request.UserId));

            if (!planItem.IsValid())
            {
                return ResultSet.Error($"Technical Definition is not valid: {planItem.GetBrokenRules().ToString()}");
            }

            planRepository.UpdateItem(planItem);

            await planRepository.UnitOfWork.SaveEntitiesAsync(planItem, cancellationToken);

            return ResultSet.Success();
        }
    }
}
