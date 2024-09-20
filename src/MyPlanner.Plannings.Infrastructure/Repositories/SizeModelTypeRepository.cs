using AutoMapper;
using BeyondNet.Ddd.Interfaces;
using Microsoft.EntityFrameworkCore;
using MyPlanner.Plannings.Api.Endpoints;
using MyPlanner.Plannings.Domain.SizeModels;
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


        public void Add(SizeModelType item)
        {
            var propsCopy = item.GetPropsCopy();

            var table = mapper.Map<SizeModelTypeTable>(propsCopy);

            foreach (var itemTable in table.Items)
            {
                itemTable.SizeModelType = table;
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

        public async void Delete(string sizeModelTypeId)
        {
            var table = await FindSizeModelTypeByIdAsync(sizeModelTypeId);

            foreach (var item in table.Items)
            {
                DeleteItem(item.Id);
            }

            table.Status = -1;
        }

        public async void ChangeCode(string sizeModelTypeId, string code)
        {
            var table = await FindSizeModelTypeByIdAsync(sizeModelTypeId);

            if (table == null)
            {
                throw new KeyNotFoundException($"Entity \"{nameof(SizeModelTypeTable)}\" ({sizeModelTypeId}) was not found.");
            }

            table.Code = code;
        }

        public async void ChangeName(string sizeModelTypeId, string name)
        {
            var table = await FindSizeModelTypeByIdAsync(sizeModelTypeId);

            if (table == null)
            {
                throw new KeyNotFoundException($"Entity \"{nameof(SizeModelTypeTable)}\" ({sizeModelTypeId}) was not found.");
            }

            table.Name = name;
        }

        public async void Activate(string sizeModelTypeId)
        {
            var table = await FindSizeModelTypeByIdAsync(sizeModelTypeId);

            if (table == null)
            {
                throw new KeyNotFoundException($"Entity \"{nameof(SizeModelTypeTable)}\" ({sizeModelTypeId}) was not found.");
            }

            table.Status = 1;
        }

        public async void Deactivate(string sizeModelTypeId)
        {
            var table = await FindSizeModelTypeByIdAsync(sizeModelTypeId);

            if (table == null)
            {
                throw new KeyNotFoundException($"Entity \"{nameof(SizeModelTypeTable)}\" ({sizeModelTypeId}) was not found.");
            }

            table.Status = 0;
        }

        public async Task<SizeModelType> GetById(string sizeModelTypeId)
        {
            var table = await FindSizeModelTypeByIdAsync(sizeModelTypeId);

            var entity = mapper.Map<SizeModelType>(table);

            return entity;
        }

        public async Task<SizeModelTypeItem> GetItemById(string sizeModelTypeId)
        {
            var table = await context.SizeModelTypeItems.FirstOrDefaultAsync(x => x.Id == sizeModelTypeId);

            var factor = mapper.Map<SizeModelTypeItem>(table);

            return factor;
        }

        public async void AddItem(SizeModelTypeItem item)
        {
            var propsCopy = item.GetPropsCopy();

            var table = mapper.Map<SizeModelTypeItemTable>(propsCopy);

            await context.SizeModelTypeItems.AddAsync(table);
        }

        public void DeleteItem(string sizeModelTypeItemId)
        {
            var table = context.SizeModelTypeItems.FirstOrDefault(x => x.Id == sizeModelTypeItemId);

            if (table == null)
            {
                throw new KeyNotFoundException($"Entity \"{nameof(SizeModelTypeItemTable)}\" ({sizeModelTypeItemId}) was not found.");
            }

            table.Status = -1;
        }

        public async void ActivateItem(string sizeModelTypeItemId)
        {
            var table = await context.SizeModelTypeItems.FirstOrDefaultAsync(x => x.Id == sizeModelTypeItemId);

            if (table == null)
            {
                throw new KeyNotFoundException($"Entity \"{nameof(SizeModelTypeItemTable)}\" ({sizeModelTypeItemId}) was not found.");
            }

            table.Status = 1;
        }

        public async void DeactivateItem(string sizeModelTypeItemId)
        {
            var table = await context.SizeModelTypeItems.FirstOrDefaultAsync(x => x.Id == sizeModelTypeItemId);

            if (table == null)
            {
                throw new KeyNotFoundException($"Entity \"{nameof(SizeModelTypeItemTable)}\" ({sizeModelTypeItemId}) was not found.");
            }

            table.Status = 0;
        }

    }
}
