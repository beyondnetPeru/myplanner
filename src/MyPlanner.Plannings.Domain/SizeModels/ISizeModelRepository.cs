using BeyondNet.Ddd.Interfaces;
using MyPlanner.Plannings.Shared.Application.Dtos;

namespace MyPlanner.Plannings.Domain.SizeModels
{
    public interface ISizeModelRepository : IRepository<SizeModel>
    {
        Task<IEnumerable<SizeModel>> GetAll(PaginationDto pagination);
        Task<SizeModel> Get(string id);
        Task Add(SizeModel sizeModel);
        Task ChangeName(string id, string name);
        Task Deactivate(string id);
        Task Activate(string id);
        Task Delete(string id);
        Task<IEnumerable<SizeModelItem>> GetItems(string sizeModelId);
        Task<SizeModelItem> GetItem(string sizeModelId, string sizeModelItemId);
        Task AddItem(SizeModelItem sizeModelItem);
        Task ChangeItemName(string sizeModelItemId, string name);
        Task DeactiveItem(string sizeModelItemId);
        Task ActiveItem(string sizeModelItemId);
        Task DeleteItem(string idsizeModelItemId);
    }
}
