using MyPlanner.Plannings.Domain.PlanAggregate;
using MyPlanner.Plannings.Infrastructure.Database;
using MyPlanner.Plannings.Infrastructure.Database.Tables;

namespace MyPlanner.Plannings.Infrastructure.Repositories
{
    public class PlanRepository : IPlanRepository
    {
        private readonly PlanningDbContext context;
        private readonly IMapper mapper;

        public IUnitOfWork UnitOfWork => context;

        public PlanRepository(IMapper mapper, PlanningDbContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        private async Task<PlanTable> GetPlanTable(string planId)
        {
            var plan = await context.Plans.FindAsync(planId);

            if (plan == null)
                throw new Exception("Plan not found");

            return plan;
        }                   

        public async Task<Plan> GetByIdAsync(string planId)
        {
            var table = await GetPlanTable(planId);

            return mapper.Map<Plan>(table);
        }

        public void Create(Plan plan)
        {
            var table = mapper.Map<PlanTable>(plan);

            context.Plans.Add(table);
        }

        public async void Update(Plan plan)
        {
            var table = await GetPlanTable(plan.GetPropsCopy().Id.GetValue());
         
            table.Owner = plan.GetPropsCopy().Owner.GetValue();
            table.Name = plan.GetPropsCopy().Name.GetValue();
            table.Status = plan.GetPropsCopy().Status.Id;
            table.SizeModelTypeId = plan.GetPropsCopy().SizeModelTypeId.GetValue();
            table.Audit.CreatedBy = plan.GetPropsCopy().Audit.GetValue().CreatedBy;
            table.Audit.CreatedAt = plan.GetPropsCopy().Audit.GetValue().CreatedAt;
            table.Audit.UpdatedBy = plan.GetPropsCopy().Audit.GetValue().UpdatedBy;
            table.Audit.UpdatedAt = plan.GetPropsCopy().Audit.GetValue().UpdatedAt;
            table.Audit.TimeSpan = plan.GetPropsCopy().Audit.GetValue().TimeSpan;
        }

        public async Task<ICollection<PlanCategory>> GetCategories(string planId)
        {
            var tables = await context.PlanCategories.AsNoTracking().Where(x => x.PlanId == planId).ToListAsync();

            var mappedTables = mapper.Map<ICollection<PlanCategory>>(tables);

            return mappedTables;
        }

        public async Task<PlanCategory> GetCategoryAsync(string planCategoryId)
        {
            var table = await context.PlanCategories.AsNoTracking().FirstAsync(x => x.Id == planCategoryId);

            var tableMapped = mapper.Map<PlanCategory>(table);

            return tableMapped;
        }

        public void AddCategory(string planId, PlanCategory category)
        {
            var table = mapper.Map<PlanCategoryTable>(category);

            table.PlanId = planId;

            context.PlanCategories.Add(table);
        }

        public void RemoveCategory(string planId, PlanCategory category)
        {
            var table = context.PlanCategories.First(x => x.Id == category.GetPropsCopy().Id.GetValue());

            if (table == null)
                throw new Exception("Category not found");

            context.PlanCategories.Remove(table);
        }

        public async Task<ICollection<PlanItem>> GetItems(string planId)
        {
            var tables = await context.PlanItems.AsNoTracking().Where(x => x.PlanId == planId).ToListAsync();

            var mappedTables = mapper.Map<ICollection<PlanItem>>(tables);

            return mappedTables;
        }

        private async Task<PlanItemTable> GetPlanItemTable(string planItemId)
        {
            var plan = await context.PlanItems.AsNoTracking().FirstAsync(x => x.Id == planItemId);

            if (plan == null)
                throw new Exception("Plan Item not found");

            return plan;
        }

        public async Task<PlanItem> GetItemById(string planItemId)
        {
            var table = await GetPlanItemTable(planItemId);

            return mapper.Map<PlanItem>(table);
        }

        public void AddItem(PlanItem planItem)
        {
            var table = mapper.Map<PlanItemTable>(planItem);

            context.PlanItems.Add(table);

            context.SaveChanges();
        }

        public async void UpdateItem(PlanItem planItem)
        {
            var table = await GetPlanItemTable(planItem.GetPropsCopy().Id.GetValue());

            table.PlanId = planItem.GetPropsCopy().PlanId.GetValue();
            table.ProductId = planItem.GetPropsCopy().ProductId.GetValue();
            table.PlanCategoryId = planItem.GetPropsCopy().PlanCategoryId.GetValue();
            table.BusinessFeatureName = planItem.GetPropsCopy().BusinessFeature.GetValue().Name;
            table.BusinessFeatureDefinition = planItem.GetPropsCopy().BusinessFeature.GetValue().BusinessDefinition;
            table.BusinessFeatureComplexityLevel = planItem.GetPropsCopy().BusinessFeature.GetValue().ComplexityLevel.Id;
            table.BusinessFeaturePriority = planItem.GetPropsCopy().BusinessFeature.GetValue().PriorityOrder;
            table.BusinessFeatureMoScoW = planItem.GetPropsCopy().BusinessFeature.GetValue().MoScoW.Id;            
            table.TechnicalDefinition = planItem.GetPropsCopy().TechnicalDefinition.GetValue();
            table.ComponentsImpacted = planItem.GetPropsCopy().ComponentsImpacted.GetValue();
            table.TechnicalDependencies = planItem.GetPropsCopy().TechnicalDependencies.GetValue();
            table.SizeModelTypeItemId = planItem.GetPropsCopy().SizeModelTypeItemId.GetValue();
            table.BallParkCostSymbol = planItem.GetPropsCopy().BallParkCosts.GetValue().BallParkCost.GetValue().Symbol.Id;
            table.BallParkCostAmount = planItem.GetPropsCopy().BallParkCosts.GetValue().BallParkCost.GetValue().Amount;
            table.BallparkDependenciesCostAmount = planItem.GetPropsCopy().BallParkCosts.GetValue().BallparkDependenciesCost.GetValue().Amount;
            table.BallParkTotalCostAmount = planItem.GetPropsCopy().BallParkCosts.GetValue().BallParkTotalCost.GetValue().Amount;
            table.KeyAssumptions = planItem.GetPropsCopy().KeyAssumptions.GetValue();
            table.UserId = planItem.GetPropsCopy().Audit.GetValue().CreatedBy;
            table.Status = planItem.GetPropsCopy().Status.Id;
            table.Audit.CreatedBy = planItem.GetPropsCopy().Audit.GetValue().CreatedBy;
            table.Audit.CreatedAt = planItem.GetPropsCopy().Audit.GetValue().CreatedAt;
            table.Audit.UpdatedBy = planItem.GetPropsCopy().Audit.GetValue().UpdatedBy;
            table.Audit.UpdatedAt = planItem.GetPropsCopy().Audit.GetValue().UpdatedAt;
            table.Audit.TimeSpan = planItem.GetPropsCopy().Audit.GetValue().TimeSpan;

        }        
    }
}
