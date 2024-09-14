using BeyondNet.Ddd.Interfaces;

namespace MyPlanner.Plannings.Domain.PlanAggregate
{
    public interface IPlanRepository : IRepository<Plan>
    {
        Task<Plan> GetByIdAsync(string planId);
        Task AddAsync(Plan plan);
        Task ChangeName(string planId, string name);
        Task ChangeOwner(string planId, string owner);
        Task DeleteAsync(Plan plan);
        Task Draft(string planId);
        Task Activate(string planId);
        Task Deactivate(string planId);
        Task Close(string planId);
        Task<PlanItem> GetItemById(string planItemId);
        Task AddItemAsync(PlanItem planItem);
        Task ChangeItemSizeModelTypeValueSelected(string planItemId, string sizeModelTypeValueSelected);
        Task ChangeItemTechnicalDefinition(string planItemId, string technicalDefinition);
        Task ChangeItemComponentsImpacted(string planItemId, string componentsImpacted);
        Task ChangeItemTechnicalDependencies(string planItemId, string technicalDependencies);
        Task ChangeItemBallParkCost(string planItemId, double ballParkCost);
        Task ChangeItemBallParkDependenciesCost(string planItemId, double ballParkDependenciesCost);
        Task ChangeItemKeyAssumptions(string planItemId, string technicalDependencies);
        Task DraftItem(string planItemId);
        Task ActivateItem(string planItemId);
        Task DeactivateItem(string planItemId);
        Task CloseItem(string planItemId);
    }
}
