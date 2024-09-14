using BeyondNet.Ddd.Interfaces;
using MyPlanner.Plannings.Domain.SizeModels;

namespace MyPlanner.Plannings.Api.Endpoints
{
    public interface ISizeModelTypeRepository : IRepository<SizeModelType>
    {
        Task<SizeModelType> GetById(string sizeModelTypeId);
        Task<SizeModelTypeFactor> GetFactorById(string sizeModelTypeId);
        Task Add(SizeModelType item);
        Task ChangeCode(string sizeModelTypeId, string code);
        Task ChangeName(string sizeModelTypeId, string name);
        Task Activate(string sizeModelTypeId);
        Task Deactivate(string sizeModelTypeId);
        Task AddFactor(SizeModelTypeFactor item);
        Task ActivateFactor(string sizeModelTypeId, string sizeModelTypeFactorId);
        Task DeactivateFactor(string sizeModelTypeId, string sizeModelTypeFactorId);
    }
}
