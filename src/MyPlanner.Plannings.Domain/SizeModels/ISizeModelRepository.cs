using BeyondNet.Ddd.Interfaces;

namespace MyPlanner.Plannings.Domain.SizeModels
{
    public interface ISizeModelRepository : IRepository<SizeModel>
    {
        Task<SizeModel> Get(string sizeModelId);
        Task<SizeModelItem> GetItem(string sizeModelItemId);
        Task Add(SizeModel sizeModel);
        Task ChangeName(string sizeModelId, string name);
        Task Deactivate(string sizeModelId);
        Task Activate(string sizeModelId);
        Task Delete(string sizeModelId);
        Task AddItem(SizeModelItem sizeModelItem);
        Task ChangeItemName(string sizeModelItemId, string name);
        Task DeactiveItem(string sizeModelItemId);
        Task ActiveItem(string sizeModelItemId);
        Task DeleteItem(string idsizeModelItemId);
    }
}
