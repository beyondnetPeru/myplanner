using BeyondNet.Ddd.Interfaces;
using MyPlanner.Plannings.Domain.SizeModels;

namespace MyPlanner.Plannings.Api.Endpoints
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
        void DeleteItem(string sizeModelTypeItemId);
        void ActivateItem(string sizeModelTypeItemId);
        void DeactivateItem(string sizeModelTypeItemId);
        void Delete(string sizeModelTypeId);
    }
}
