namespace MyPlanner.Plannings.Domain.SizeModels
{
    public interface ISizeModelRepository : IRepository<SizeModel>
    {
        Task<ICollection<SizeModel>> GetAll();
        Task<SizeModel> Get(string sizeModelId);
        void Add(SizeModel sizeModel);
        void Update(SizeModel sizeModel);
        void Delete(string sizeModelId);

        Task<ICollection<SizeModelItem>> GetAllItem(string sizeModelId);
        Task<SizeModelItem> GetItem(string sizeModelItemId);
        void UpdateItem(SizeModelItem sizeModelItem);
        void AddItem(SizeModelItem sizeModelItem);
        Task DeleteItem(string sizeModelId, string sizeModelItemId);        
    }
}
