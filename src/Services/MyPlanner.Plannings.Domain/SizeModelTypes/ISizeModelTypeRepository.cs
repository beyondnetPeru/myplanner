using BeyondNet.Ddd.Interfaces;

namespace MyPlanner.Plannings.Domain.SizeModelTypes
{
    public interface ISizeModelTypeRepository : IRepository<SizeModelType>
    {
        Task<SizeModelType> GetById(string sizeModelTypeId);
        Task<SizeModelTypeItem> GetItemById(string sizeModelTypeId);
        void Add(SizeModelType item);
        void ChangeCode(string sizeModelTypeId, string code);
        void ChangeName(string sizeModelTypeId, string name);
        void Activate(string sizeModelTypeId);
        void Deactivate(string sizeModelTypeId);
        void AddItem(SizeModelTypeItem item);
        void ChangeItemCode(string sizeModelTypeItemId, string code);
        void ChangeItemName(string sizeModelTypeItemId, string name);
        void DeleteItem(string sizeModelTypeItemId);
        void ActivateItem(string sizeModelTypeItemId);
        void DeactivateItem(string sizeModelTypeItemId);
        void Delete(string sizeModelTypeId);
    }
}
