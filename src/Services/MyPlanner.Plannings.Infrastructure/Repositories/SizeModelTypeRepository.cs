using MyPlanner.Plannings.Domain.SizeModelTypes;
using MyPlanner.Plannings.Infrastructure.Database;
using MyPlanner.Plannings.Infrastructure.Database.Tables;

namespace MyPlanner.Plannings.Infrastructure.Repositories
{
    public class SizeModelTypeRepository : ISizeModelTypeRepository
    {
        private readonly PlanningDbContext context;
        private readonly IMapper mapper;

        public IUnitOfWork UnitOfWork => context;

        public SizeModelTypeRepository(PlanningDbContext context, IMapper mapper)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<SizeModelType> GetById(string sizeModelTypeId)
        {
            var table = await FindSizeModelTypeByIdAsync(sizeModelTypeId);

            var entity = mapper.Map<SizeModelType>(table);

            return entity;
        }

    
        public void Add(SizeModelType item)
        {
            var propsCopy = item.GetPropsCopy();

            var table = mapper.Map<SizeModelTypeTable>(propsCopy);

            foreach (var itemTable in table.Items)
            {
                itemTable.SizeModelType = table;
                itemTable.SizeModelTypeId = table.Id;   
            }

            context.SizeModelTypes.Add(table);
        }

        private async Task<SizeModelTypeTable> FindSizeModelTypeByIdAsync(string sizeModelTypeId)
        {
            var table = await context.SizeModelTypes.FirstOrDefaultAsync(x => x.Id == sizeModelTypeId);

            if (table == null)
            {
                throw new KeyNotFoundException($"Entity \"{nameof(SizeModelTypeTable)}\" ({sizeModelTypeId}) was not found.");
            }

            return table;
        }

        private async Task<SizeModelTypeItemTable> FindSizeModelTypeItemByIdAsync(string sizeModelTypeId, string sizeModelTypeItemId)
        {
            var table = await context.SizeModelTypeItems.FirstOrDefaultAsync(x => x.Id == sizeModelTypeItemId && x.SizeModelTypeId == sizeModelTypeId);

            if (table == null)
            {
                throw new KeyNotFoundException($"Entity \"{nameof(SizeModelTypeTable)}\" ({sizeModelTypeId}) was not found.");
            }

            return table;
        }

        public async void Update(SizeModelType item)
        {
            var table = await FindSizeModelTypeByIdAsync(item.GetPropsCopy().Id.GetValue());

            table.Code = item.GetPropsCopy().Code.GetValue();
            table.Name = item.GetPropsCopy().Name.GetValue();
            table.Status = item.GetPropsCopy().Status.Id;
        }

        public async void Delete(string sizeModelTypeId)
        {
            var table = await FindSizeModelTypeByIdAsync(sizeModelTypeId);

            foreach (var item in table.Items)
            {
                DeleteItem(item.SizeModelTypeId, item.Id);
            }

            table.Status = 0;
        }

        public async Task<SizeModelTypeItem> GetItemById(string sizeModelTypeId)
        {
            var table = await context.SizeModelTypeItems.FirstOrDefaultAsync(x => x.Id == sizeModelTypeId);

            return mapper.Map<SizeModelTypeItem>(table);
        }


        public async void AddItem(SizeModelTypeItem item)
        {
            var propsCopy = item.GetPropsCopy();

            var table = mapper.Map<SizeModelTypeItemTable>(propsCopy);

            await context.SizeModelTypeItems.AddAsync(table);
        }

        public void DeleteItem(string sizeModelTypeId, string sizeModelTypeItemId)
        {
            var table = context.SizeModelTypeItems.FirstOrDefault(x => x.SizeModelTypeId == sizeModelTypeId && x.Id == sizeModelTypeItemId);

            if (table == null)
            {
                throw new KeyNotFoundException($"Entity \"{nameof(SizeModelTypeItemTable)}\" ({sizeModelTypeItemId}) was not found.");
            }

            table.Status = 0;
        }

        public async void UpdateItem(SizeModelTypeItem modelTypeItem)
        {
            var table = await FindSizeModelTypeItemByIdAsync(modelTypeItem.GetPropsCopy().SizeModelType.GetPropsCopy().Id.GetValue(), modelTypeItem.GetPropsCopy().Id.GetValue());

            if (table == null)
            {
                throw new KeyNotFoundException($"Entity \"{nameof(SizeModelTypeItemTable)}\" ID was not found.");
            }

            table.Name = modelTypeItem.GetPropsCopy().Name.GetValue();
            table.Code = modelTypeItem.GetPropsCopy().Code.GetValue();
            table.Status = modelTypeItem.GetPropsCopy().Status.Id;
            table.SizeModelTypeId = modelTypeItem.GetPropsCopy().SizeModelType.GetPropsCopy().Id.GetValue();            
        }
    }
}
