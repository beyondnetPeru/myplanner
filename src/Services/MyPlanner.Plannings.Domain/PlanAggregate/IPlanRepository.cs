namespace MyPlanner.Plannings.Domain.PlanAggregate
{
    public interface IPlanRepository : IRepository<Plan>
    {
        Task<Plan> GetByIdAsync(string planId);
        void Create(Plan plan);        
        void Update(Plan plan);
    
        Task<ICollection<PlanCategory>> GetCategories(string planId);
        Task<PlanCategory> GetCategoryAsync(string planCategoryId);
        Task<PlanCategory> GetCategoryByName(string planId, string categoryName);
        void AddCategory(string planId, PlanCategory category);
        void RemoveCategory(string planId, PlanCategory category);

        Task<ICollection<PlanItem>> GetItems(string planId);
        Task<PlanItem> GetItemById(string planItemId);
        void AddItem(PlanItem planItem);
        void UpdateItem(PlanItem planItem);
    }
}
