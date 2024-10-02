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

        public async void ChangeName(string planId, string name)
        {
            var table = await GetPlanTable(planId);

            table.Name = name;
        }

        public async void ChangeOwner(string planId, string owner)
        {
            var table = await GetPlanTable(planId);

            table.Owner = owner;
        }

        public async void ChangeStatus(string planId, int status)
        {
            var table = await GetPlanTable(planId);

            table.Status = status;
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
        }

        public async void ChangeItemTechnicalDefinition(string planItemId, string technicalDefinition)
        {
            var table = await GetPlanItemTable(planItemId);

            table.TechnicalDefinition = technicalDefinition;
        }

        public async void ChangeItemComponentsImpacted(string planItemId, string componentsImpacted)
        {
            var table = await GetPlanItemTable(planItemId);

            table.ComponentsImpacted = componentsImpacted;
        }

        public async void ChangeItemTechnicalDependencies(string planItemId, string technicalDependencies)
        {
            var table = await GetPlanItemTable(planItemId);

            table.TechnicalDependencies = technicalDependencies;
        }

        public async void ChangeItemBallParkCost(string planItemId,int symbol, double ballParkCost, double ballParkTotalCost)
        {
            var table = await GetPlanItemTable(planItemId);

            table.BallParkCostAmount = ballParkCost;
            table.BallParkCostSymbol = symbol;
            table.BallParkTotalCostSymbol = symbol;
        }

        public async void ChangeItemBallParkDependenciesCost(string planItemId, int symbol, double ballParkDependenciesCost, double ballParkTotalCost)
        {
            var table = await GetPlanItemTable(planItemId);

            table.BallparkDependenciesCostAmount = ballParkDependenciesCost;
            table.BallparkDependenciesCostSymbol = symbol;
            table.BallParkTotalCostSymbol = symbol;
        }

        public async void ChangeItemKeyAssumptions(string planItemId, string technicalDependencies)
        {
            var table = await GetPlanItemTable(planItemId);

            table.KeyAssumptions = technicalDependencies;
        }

        public async void ChangeItemStatus(string planItemId, int status)
        {
            var table = await GetPlanItemTable(planItemId);

            table.Status = status;
        }


        public async void ChangeItemSizeModelTypeItemId(string planItemId, string sizeModelTypeItemId)
        {
            var table = await GetPlanItemTable(planItemId);

            table.SizeModelTypeItemId = sizeModelTypeItemId;
        }

        public async void ChangeItemBallParkTotalCost(string planItemId, double ballParkTotalCost)
        {
            var table = await GetPlanItemTable(planItemId);

            table.BallParkTotalCostAmount = ballParkTotalCost;
        }

    }
}
