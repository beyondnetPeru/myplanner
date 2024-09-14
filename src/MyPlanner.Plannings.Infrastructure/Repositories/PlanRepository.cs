using AutoMapper;
using BeyondNet.Ddd.Interfaces;
using Microsoft.EntityFrameworkCore;
using MyPlanner.Plannings.Domain.PlanAggregate;
using MyPlanner.Plannings.Infrastructure.Database;
using MyPlanner.Plannings.Shared.Application.Dtos;

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

        public IUnitOfWork UnitOfWork => context;

        public Task Activate(string planId)
        {
            throw new NotImplementedException();
        }

        public Task AddAsync(Plan plan)
        {
            throw new NotImplementedException();
        }

        public Task ChangeName(string planId, string name)
        {
            throw new NotImplementedException();
        }

        public Task ChangeOwner(string planId, string owner)
        {
            throw new NotImplementedException();
        }

        public Task Close(string planId)
        {
            throw new NotImplementedException();
        }

        public Task Deactivate(string planId)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Plan plan)
        {
            throw new NotImplementedException();
        }

        public Task Draft(string planId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Plan>> GetAllAsync(PaginationDto pagination)
        {
            var data = await context.Plans
                .OrderBy(c => c.Name)
                .Skip(pagination.RecordsPerPage * pagination.Page)
                .Take(pagination.RecordsPerPage)
                .ToListAsync();

            var entities = mapper.Map<ICollection<Plan>>(data);

            return entities;
        }

        public Task<Plan> GetByIdAsync(string planId)
        {
            throw new NotImplementedException();
        }
    }
}
