using BeyondNet.Ddd.Interfaces;

namespace MyPlanner.Plannings.Domain.SizeModels
{
    public interface ISizeModelTypeFactorRepository : IRepository<SizeModelTypeFactor>
    {
        Task<IEnumerable<SizeModelTypeFactor>> GetAll(string sizeModelTypeId);
        Task<SizeModelTypeFactor> GetFactorById(string id);
        Task<SizeModelTypeFactor> GetFactorByCode(string code);
        Task Add(SizeModelTypeFactor item);
        Task Activate(SizeModelTypeFactor item);
        Task Deactivate(SizeModelTypeFactor item);
    }
}
