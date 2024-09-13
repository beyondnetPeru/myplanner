using BeyondNet.Ddd.Interfaces;
using MyPlanner.Plannings.Shared.Application.Dtos;

namespace MyPlanner.Plannings.Domain.PlanAggregate
{
    public interface IPlanRepository : IRepository<Plan>
    {
        Task<IEnumerable<Plan>> GetAllAsync(PaginationDto pagination);
        Task<Plan> GetByIdAsync(string planId);
        Task AddAsync(Plan plan);
        Task ChangeName(string planId, string name);
        Task ChangeOwner(string planId, string owner);
        Task DeleteAsync(Plan plan);
        Task Draft(string planId);
        Task Activate(string planId);
        Task Deactivate(string planId);
        Task Close(string planId);
    }
}
