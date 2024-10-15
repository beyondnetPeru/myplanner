namespace MyPlanner.Plannings.Domain.SizeModelTypes
{
    public interface ISizeModelTypeRepository : IRepository<SizeModelType>
    {
        Task<SizeModelType> GetById(string sizeModelTypeId);        
        void Add(SizeModelType item);
        void Update(SizeModelType item);
        void Delete(string sizeModelTypeId);

        Task<SizeModelTypeItem> GetItemById(string sizeModelTypeId);
        void AddItem(SizeModelTypeItem item);
        void UpdateItem(SizeModelTypeItem item);
        void DeleteItem(string sizeModelTypeId, string sizeModelTypeItemId);

    }
}
