using BeyondNet.Ddd.Interfaces;

namespace MyPlanner.Plannings.Domain.PlanAggregate
{
    public interface IPlanRepository : IRepository<Plan>
    {
        Task<Plan> GetByIdAsync(string planId);
        void Create(Plan plan);
        void ChangeName(string planId, string name);
        void ChangeOwner(string planId, string owner);
        void Delete(Plan plan);
        void Draft(string planId);
        void Activate(string planId);
        void Deactivate(string planId);
        void Close(string planId);
        Task<PlanItem> GetItemById(string planItemId);
        void AddItem(PlanItem planItem);
        void ChangeItemSizeModelTypeValueSelected(string planItemId, string sizeModelTypeValueSelected);
        void ChangeItemTechnicalDefinition(string planItemId, string technicalDefinition);
        void ChangeItemComponentsImpacted(string planItemId, string componentsImpacted);
        void ChangeItemTechnicalDependencies(string planItemId, string technicalDependencies);
        void ChangeItemBallParkCost(string planItemId, double ballParkCost);
        void ChangeItemBallParkDependenciesCost(string planItemId, double ballParkDependenciesCost);
        void ChangeItemKeyAssumptions(string planItemId, string technicalDependencies);
        void DraftItem(string planItemId);
        void ActivateItem(string planItemId);
        void DeactivateItem(string planItemId);
        void CloseItem(string planItemId);
    }
}
