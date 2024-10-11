using MyPlanner.Plannings.Domain.PlanAggregate;
using MyPlanner.Shared.Cqrs;
using MyPlanner.Shared.Domain.ValueObjects;

namespace MyPlanner.Plannings.Api.UseCases.Plan.Command.ChangeChangeSizeModelTypeItem
{
    public class ChangeChangeSizeModelTypeItemCommandHandler : AbstractCommandHandler<ChangeChangeSizeModelTypeItemCommand, ResultSet>
    {
        private readonly IPlanRepository planRepository;

        public ChangeChangeSizeModelTypeItemCommandHandler(IPlanRepository planRepository, ILogger<ChangeChangeSizeModelTypeItemCommandHandler> Logger) : base(Logger)
        {
            this.planRepository = planRepository ?? throw new ArgumentNullException(nameof(planRepository));
        }

        public async override Task<ResultSet> HandleCommand(ChangeChangeSizeModelTypeItemCommand request, CancellationToken cancellationToken)
        {
            var planItem = await planRepository.GetItemById(request.PlanItemId);

            planItem.ChangeSizeModelTypeItemId(IdValueObject.Create(request.SizeModelTypeItemId), UserId.Create(request.UserId));

            if (!planItem.IsValid())
            {
                return ResultSet.Error(
                    $"Error changing size model type item with id {request.SizeModelTypeItemId} for plan item with id {request.PlanItemId}. Errors: {planItem.GetBrokenRules().ToString()}");
            }
            
            planRepository.UpdateItem(planItem);

            await planRepository.UnitOfWork.SaveEntitiesAsync(planItem, cancellationToken);

            return ResultSet.Success();
        }
    }
}
