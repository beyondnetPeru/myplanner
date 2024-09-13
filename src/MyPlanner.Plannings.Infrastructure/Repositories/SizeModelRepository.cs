using BeyondNet.Ddd.Interfaces;
using MyPlanner.Plannings.Domain.SizeModels;
using MyPlanner.Plannings.Shared.Application.Dtos;

namespace MyPlanner.Plannings.Infrastructure.Repositories
{
    public class SizeModelRepository : ISizeModelRepository
    {
        public IUnitOfWork UnitOfWork => throw new NotImplementedException();

        public Task Activate(string id)
        {
            throw new NotImplementedException();
        }

        public Task ActiveItem(string sizeModelItemId)
        {
            throw new NotImplementedException();
        }

        public Task Add(SizeModel sizeModel)
        {
            throw new NotImplementedException();
        }

        public Task AddItem(SizeModelItem sizeModelItem)
        {
            throw new NotImplementedException();
        }

        public Task ChangeItemName(string sizeModelItemId, string name)
        {
            throw new NotImplementedException();
        }

        public Task ChangeName(string id, string name)
        {
            throw new NotImplementedException();
        }

        public Task Deactivate(string id)
        {
            throw new NotImplementedException();
        }

        public Task DeactiveItem(string sizeModelItemId)
        {
            throw new NotImplementedException();
        }

        public Task Delete(string id)
        {
            throw new NotImplementedException();
        }

        public Task DeleteItem(string idsizeModelItemId)
        {
            throw new NotImplementedException();
        }

        public Task<SizeModel> Get(string id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<SizeModel>> GetAll(PaginationDto pagination)
        {
            throw new NotImplementedException();
        }

        public Task<SizeModelItem> GetItem(string sizeModelId, string sizeModelItemId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<SizeModelItem>> GetItems(string sizeModelId)
        {
            throw new NotImplementedException();
        }
    }
}
