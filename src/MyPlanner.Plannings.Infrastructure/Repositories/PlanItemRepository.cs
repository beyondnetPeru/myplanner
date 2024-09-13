using BeyondNet.Ddd.Interfaces;
using MyPlanner.Plannings.Domain.PlanAggregate;

namespace MyPlanner.Plannings.Infrastructure.Repositories
{
    public class PlanItemRepository : IPlanItemRepository
    {
        public IUnitOfWork UnitOfWork => throw new NotImplementedException();

        public Task Activate(string planItemId)
        {
            throw new NotImplementedException();
        }

        public Task AddAsync(PlanItem planItem)
        {
            throw new NotImplementedException();
        }

        public Task ChangeBallParkCost(double ballParkCost)
        {
            throw new NotImplementedException();
        }

        public Task ChangeBallParkDependenciesCost(double ballParkDependenciesCost)
        {
            throw new NotImplementedException();
        }

        public Task ChangeComponentsImpacted(string componentsImpacted)
        {
            throw new NotImplementedException();
        }

        public Task ChangeKeyAssumptions(string technicalDependencies)
        {
            throw new NotImplementedException();
        }

        public Task ChangeSizeModelTypeValueSelected(string sizeModelTypeValueSelected)
        {
            throw new NotImplementedException();
        }

        public Task ChangeTechnicalDefinition(string technicalDefinition)
        {
            throw new NotImplementedException();
        }

        public Task ChangeTechnicalDependencies(string technicalDependencies)
        {
            throw new NotImplementedException();
        }

        public Task Close(string planItemId)
        {
            throw new NotImplementedException();
        }

        public Task Deactivate(string planItemId)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(PlanItem planItem)
        {
            throw new NotImplementedException();
        }

        public Task Draft(string planItemId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<PlanItem>> GetAllAsync(string planId)
        {
            throw new NotImplementedException();
        }

        public Task<PlanItem> GetByIdAsync(string planId, string planItemId)
        {
            throw new NotImplementedException();
        }
    }
}
