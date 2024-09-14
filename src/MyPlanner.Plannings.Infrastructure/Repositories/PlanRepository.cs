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

        public PlanRepository(IMapper mapper, PlanningDbContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        private async Task<PlanTable> GetPlanTable(string planId)
        {
            return await context.Plans.FirstAsync(x => x.Id == planId);
        }

        private async Task<PlanItemTable> GetPlanItemTable(string planItemId)
        {
            return await context.PlanItems.FirstAsync(x => x.Id == planItemId);
        }

        public IUnitOfWork UnitOfWork => context;

        public async Task Activate(string planId)
        {
            var table = await GetPlanTable(planId);

            table.Status = 1;
        }

        public async Task<Plan> GetByIdAsync(string planId)
        {
            var table = await GetPlanTable(planId);

            return mapper.Map<Plan>(table);
        }

        public async Task Create(Plan plan)
        {
            var table = mapper.Map<PlanTable>(plan);

            await context.Plans.AddAsync(table);
        }

        public async Task ChangeName(string planId, string name)
        {
            var table = await GetPlanTable(planId);

            table.Name = name;
        }

        public async Task ChangeOwner(string planId, string owner)
        {
            var table = await GetPlanTable(planId);

            table.Owner = owner;
        }

        public async Task Delete(Plan plan)
        {
            var table = mapper.Map<PlanTable>(plan);

            context.Plans.Remove(table);
        }

        public async Task Draft(string planId)
        {
            var table = await GetPlanTable(planId);

            table.Status = 0;
        }

        public async Task Deactivate(string planId)
        {
            var table = await GetPlanTable(planId);

            table.Status = 2;
        }

        public async Task Close(string planId)
        {
            var table = await GetPlanTable(planId);

            table.Status = 3;
        }

        public async Task<PlanItem> GetItemById(string planItemId)
        {
            var table = await GetPlanItemTable(planItemId);

            return mapper.Map<PlanItem>(table);
        }

        public async Task AddItemAsync(PlanItem planItem)
        {
            var table = mapper.Map<PlanItemTable>(planItem);

            await context.PlanItems.AddAsync(table);
        }

        public async Task ChangeItemSizeModelTypeValueSelected(string planItemId, string sizeModelTypeValueSelected)
        {
            var table = await GetPlanItemTable(planItemId);

            table.SizeModelTypeValueSelected = sizeModelTypeValueSelected;
        }

        public async Task ChangeItemTechnicalDefinition(string planItemId, string technicalDefinition)
        {
            var table = await GetPlanItemTable(planItemId);

            table.TechnicalDefinition = technicalDefinition;
        }

        public async Task ChangeItemComponentsImpacted(string planItemId, string componentsImpacted)
        {
            var table = await GetPlanItemTable(planItemId);

            table.ComponentsImpacted = componentsImpacted;
        }

        public async Task ChangeItemTechnicalDependencies(string planItemId, string technicalDependencies)
        {
            var table = await GetPlanItemTable(planItemId);

            table.TechnicalDependencies = technicalDependencies;
        }

        public async Task ChangeItemBallParkCost(string planItemId, double ballParkCost)
        {
            var table = await GetPlanItemTable(planItemId);

            table.BallParkCost = ballParkCost;
        }

        public async Task ChangeItemBallParkDependenciesCost(string planItemId, double ballParkDependenciesCost)
        {
            var table = await GetPlanItemTable(planItemId);

            table.BallParkDependenciesCost = ballParkDependenciesCost;
        }

        public async Task ChangeItemKeyAssumptions(string planItemId, string technicalDependencies)
        {
            var table = await GetPlanItemTable(planItemId);

            table.KeyAssumptions = technicalDependencies;
        }

        public async Task DraftItem(string planItemId)
        {
            var table = await GetPlanItemTable(planItemId);

            table.Status = 0;
        }

        public async Task ActivateItem(string planItemId)
        {
            var table = await GetPlanItemTable(planItemId);

            table.Status = 1;
        }

        public async Task DeactivateItem(string planItemId)
        {
            var table = await GetPlanItemTable(planItemId);

            table.Status = 2;
        }

        public async Task CloseItem(string planItemId)
        {
            var table = await GetPlanItemTable(planItemId);

            table.Status = 3;
        }

    }
}
