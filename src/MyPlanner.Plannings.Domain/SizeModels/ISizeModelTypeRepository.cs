using BeyondNet.Ddd.Interfaces;
using MyPlanner.Plannings.Domain.SizeModels;
using MyPlanner.Plannings.Shared.Application.Dtos;

namespace MyPlanner.Plannings.Api.Endpoints
{
    public interface ISizeModelTypeRepository : IRepository<SizeModelType>
    {
        Task<IEnumerable<SizeModelType>> GetAll(PaginationDto paginationDto);
        Task<SizeModelType> GetById(string id);
        Task<SizeModelType> GetByCode(string code);
        Task Add(SizeModelType item);
        Task ChangeCode(string id, string code);
        Task ChangeName(string id, string name);
        Task Activate(string id);
        Task Deactivate(string id);
    }
}
