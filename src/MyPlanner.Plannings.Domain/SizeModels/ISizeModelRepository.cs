using BeyondNet.Ddd.Interfaces;

namespace MyPlanner.Plannings.Domain.SizeModels
{
    public interface ISizeModelRepository : IRepository<SizeModel>
    {
        Task<SizeModel> Get(string sizeModelId);
        void Add(SizeModel sizeModel);
        void ChangeName(string sizeModelId, string name);
        void Deactivate(string sizeModelId);
        void Activate(string sizeModelId);
        void Delete(string sizeModelId);

        Task<SizeModelItem> GetItem(string sizeModelItemId);
        void AddItem(SizeModelItem sizeModelItem);
        void ChangeSizeModelTypeItem(string sizeModelItemId, string sizeModelItemTypeId);
        void ChangeFactorSelected(string sizeModelItemId, int factorSelected);
        void ChangeQuantity(string sizeModelItemId, int quantity);
        void DeactiveItem(string sizeModelItemId);
        void ActiveItem(string sizeModelItemId);
        void DeleteItem(string idsizeModelItemId);
        void ChangeTotalCost(string sizeModelItemId, double totalCost);
        void ChangeIsStandard(string sizeModelItemId, bool isStandard);
    }
}
