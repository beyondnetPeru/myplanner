using AutoMapper;
using BeyondNet.Ddd.Interfaces;
using Microsoft.EntityFrameworkCore;
using MyPlanner.Plannings.Domain.SizeModels;
using MyPlanner.Plannings.Infrastructure.Database;
using MyPlanner.Plannings.Infrastructure.Database.Tables;

namespace MyPlanner.Plannings.Infrastructure.Repositories
{
    public class SizeModelRepository : ISizeModelRepository
    {
        private readonly PlanningDbContext context;
        private readonly IMapper mapper;

        public SizeModelRepository(PlanningDbContext context, IMapper mapper)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public IUnitOfWork UnitOfWork => context;


        private async Task<SizeModelTable> GetSizeModel(string sizeModelId)
        {
            return await context.SizeModels.FirstAsync(x => x.Id == sizeModelId);
        }

        private async Task<SizeModelItemTable> GetSizeItemModel(string sizeModelItemId)
        {
            return await context.SizeModelItems.FirstAsync(x => x.Id == sizeModelItemId);
        }


        public async Task Activate(string id)
        {
            var table = await GetSizeModel(id);

            table.Status = 1;
        }

        public async Task ActiveItem(string sizeModelItemId)
        {
            var table = await GetSizeItemModel(sizeModelItemId);

            table.Status = 2;
        }

        public async Task Add(SizeModel sizeModel)
        {
            var table = mapper.Map<SizeModelTable>(sizeModel);

            await context.SizeModels.AddAsync(table);
        }

        public async Task AddItem(SizeModelItem sizeModelItem)
        {
            var table = mapper.Map<SizeModelItemTable>(sizeModelItem);

            await context.SizeModelItems.AddAsync(table);
        }

        public async Task ChangeItemName(string sizeModelItemId, string name)
        {
            var table = await GetSizeItemModel(sizeModelItemId);

            table.Name = name;
        }

        public async Task ChangeName(string sizeModelId, string name)
        {
            var table = await GetSizeModel(sizeModelId);

            table.Name = name;
        }

        public async Task Deactivate(string sizeModelId)
        {
            var table = await GetSizeModel(sizeModelId);

            table.Status = 1;
        }

        public async Task DeactiveItem(string sizeModelItemId)
        {
            var table = await GetSizeItemModel(sizeModelItemId);

            table.Status = 2;
        }

        public async Task Delete(string sizeModelId)
        {
            var table = await GetSizeModel(sizeModelId);

            context.SizeModels.Remove(table);
        }

        public async Task DeleteItem(string sizeModelItemId)
        {
            var table = await GetSizeItemModel(sizeModelItemId);

            context.SizeModelItems.Remove(table);
        }

        public async Task<SizeModel> Get(string sizeModelId)
        {
            var data = await context.SizeModels.FindAsync(sizeModelId);

            var dto = mapper.Map<SizeModel>(data);

            return dto;
        }

        public async Task<SizeModelItem> GetItem(string sizeModelItemId)
        {
            var data = await context.SizeModelItems.FindAsync(sizeModelItemId);

            var dto = mapper.Map<SizeModelItem>(data);

            return dto;
        }
    }
}
