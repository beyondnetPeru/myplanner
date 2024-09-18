using BeyondNet.Ddd.Interfaces;

namespace MyPlanner.Plannings.Domain.SizeModels
{
    public interface ISizeModelRepository : IRepository<SizeModel>
    {
        Task<SizeModel> Get(string sizeModelId);
        Task<SizeModelItem> GetItem(string sizeModelItemId);
        void Add(SizeModel sizeModel);
        void ChangeName(string sizeModelId, string name);
        void Deactivate(string sizeModelId);
        void Activate(string sizeModelId);
        void Delete(string sizeModelId);
        void AddItem(SizeModelItem sizeModelItem);
        void ChangeItemName(string sizeModelItemId, string name);
        void DeactiveItem(string sizeModelItemId);
        void ActiveItem(string sizeModelItemId);
        void DeleteItem(string idsizeModelItemId);
    }
}
