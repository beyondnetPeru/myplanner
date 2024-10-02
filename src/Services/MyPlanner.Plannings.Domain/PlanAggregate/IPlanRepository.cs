namespace MyPlanner.Plannings.Domain.PlanAggregate
{
    public interface IPlanRepository : IRepository<Plan>
    {
        Task<Plan> GetByIdAsync(string planId);
        void Create(Plan plan);        
        void ChangeName(string planId, string name);
        void ChangeOwner(string planId, string owner);        
        void ChangeStatus(string planId, int status);

        Task<ICollection<PlanCategory>> GetCategories(string planId);
        Task<PlanCategory> GetCategoryAsync(string planCategoryId);
        void AddCategory(string planId, PlanCategory category);
        void RemoveCategory(string planId, PlanCategory category);
        
        Task<PlanItem> GetItemById(string planItemId);
        void AddItem(PlanItem planItem);
        void ChangeItemSizeModelTypeItemId(string planItemId, string sizeModelTypeItemId);
        void ChangeItemTechnicalDefinition(string planItemId, string technicalDefinition);
        void ChangeItemComponentsImpacted(string planItemId, string componentsImpacted);
        void ChangeItemTechnicalDependencies(string planItemId, string technicalDependencies);
        void ChangeItemBallParkCost(string planItemId, int symbol, double ballParkCost, double ballParkTotalCost);
        void ChangeItemBallParkDependenciesCost(string planItemId, int symbol, double ballParkDependenciesCost, double ballParkTotalCost);        
        void ChangeItemKeyAssumptions(string planItemId, string technicalAssumptions);
        void ChangeItemStatus(string planItemId, int status);
    }
}
