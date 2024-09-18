using AutoMapper;
using BeyondNet.Ddd.Interfaces;
using Microsoft.EntityFrameworkCore;
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

        private async Task<PlanItemTable> GetPlanItemTable(string planItemId)
        {
            var plan = await context.PlanItems.AsNoTracking().FirstAsync(x => x.Id == planItemId);

            if (plan == null)
                throw new Exception("Plan Item not found");

            return plan;
        }

        public async void Activate(string planId)
        {
            var table = await GetPlanTable(planId);

            table.Status = 1;
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

        public void Delete(Plan plan)
        {
            var table = mapper.Map<PlanTable>(plan);

            context.Plans.Remove(table);
        }

        public async void Draft(string planId)
        {
            var table = await GetPlanTable(planId);

            table.Status = 0;
        }

        public async void Deactivate(string planId)
        {
            var table = await GetPlanTable(planId);

            table.Status = 2;
        }

        public async void Close(string planId)
        {
            var table = await GetPlanTable(planId);

            table.Status = 3;
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

        public async void ChangeItemSizeModelTypeValueSelected(string planItemId, string sizeModelTypeValueSelected)
        {
            var table = await GetPlanItemTable(planItemId);

            table.SizeModelTypeValueSelected = sizeModelTypeValueSelected;
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

        public async void ChangeItemBallParkCost(string planItemId, double ballParkCost)
        {
            var table = await GetPlanItemTable(planItemId);

            table.BallParkCost = ballParkCost;
        }

        public async void ChangeItemBallParkDependenciesCost(string planItemId, double ballParkDependenciesCost)
        {
            var table = await GetPlanItemTable(planItemId);

            table.BallParkDependenciesCost = ballParkDependenciesCost;
        }

        public async void ChangeItemKeyAssumptions(string planItemId, string technicalDependencies)
        {
            var table = await GetPlanItemTable(planItemId);

            table.KeyAssumptions = technicalDependencies;
        }

        public async void DraftItem(string planItemId)
        {
            var table = await GetPlanItemTable(planItemId);

            table.Status = 0;
        }

        public async void ActivateItem(string planItemId)
        {
            var table = await GetPlanItemTable(planItemId);

            table.Status = 1;
        }

        public async void DeactivateItem(string planItemId)
        {
            var table = await GetPlanItemTable(planItemId);

            table.Status = 2;
        }

        public async void CloseItem(string planItemId)
        {
            var table = await GetPlanItemTable(planItemId);

            table.Status = 3;
        }

    }
}
