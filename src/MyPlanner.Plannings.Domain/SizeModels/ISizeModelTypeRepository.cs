using BeyondNet.Ddd.Interfaces;
using MyPlanner.Plannings.Domain.SizeModels;

namespace MyPlanner.Plannings.Api.Endpoints
{
    public interface ISizeModelTypeRepository : IRepository<SizeModelType>
    {
        Task<SizeModelType> GetById(string sizeModelTypeId);
        Task<SizeModelTypeFactor> GetFactorById(string sizeModelTypeId);
        void Add(SizeModelType item);
        void ChangeCode(string sizeModelTypeId, string code);
        void ChangeName(string sizeModelTypeId, string name);
        void Activate(string sizeModelTypeId);
        void Deactivate(string sizeModelTypeId);
        void AddFactor(SizeModelTypeFactor item);
        void ActivateFactor(string sizeModelTypeId, string sizeModelTypeFactorId);
        void DeactivateFactor(string sizeModelTypeId, string sizeModelTypeFactorId);
    }
}
