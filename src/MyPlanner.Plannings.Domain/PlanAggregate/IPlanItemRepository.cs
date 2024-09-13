using BeyondNet.Ddd.Interfaces;

namespace MyPlanner.Plannings.Domain.PlanAggregate
{
    public interface IPlanItemRepository : IRepository<PlanItem>
    {
        Task<IEnumerable<PlanItem>> GetAllAsync(string planId);
        Task<PlanItem> GetByIdAsync(string planId, string planItemId);
        Task AddAsync(PlanItem planItem);
        Task ChangeSizeModelTypeValueSelected(string sizeModelTypeValueSelected);
        Task ChangeTechnicalDefinition(string technicalDefinition);
        Task ChangeComponentsImpacted(string componentsImpacted);
        Task ChangeTechnicalDependencies(string technicalDependencies);
        Task ChangeBallParkCost(double ballParkCost);
        Task ChangeBallParkDependenciesCost(double ballParkDependenciesCost);
        Task ChangeKeyAssumptions(string technicalDependencies);
        Task Draft(string planItemId);
        Task Activate(string planItemId);
        Task Deactivate(string planItemId);
        Task Close(string planItemId);
        Task DeleteAsync(PlanItem planItem);
    }
}
