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

        private async Task<SizeModelItemTable> GetSizeModelItem(string sizeModelItemId)
        {
            return await context.SizeModelItems.FirstAsync(x => x.Id == sizeModelItemId);
        }


        public async void Activate(string id)
        {
            var table = await GetSizeModel(id);

            table.Status = 1;
        }

        public async void ActiveItem(string sizeModelItemId)
        {
            var table = await GetSizeModelItem(sizeModelItemId);

            table.Status = 2;
        }

        public void Add(SizeModel sizeModel)
        {
            var sizeModelProps = sizeModel.GetPropsCopy();

            var table = mapper.Map<SizeModelTable>(sizeModelProps);

            context.SizeModels.Add(table);
        }

        public void AddItem(SizeModelItem sizeModelItem)
        {
            var table = mapper.Map<SizeModelItemTable>(sizeModelItem);

            context.SizeModelItems.Add(table);
        }


        public async void ChangeName(string sizeModelId, string name)
        {
            var table = await GetSizeModel(sizeModelId);

            table.Name = name;
        }

        public async void Deactivate(string sizeModelId)
        {
            var table = await GetSizeModel(sizeModelId);

            table.Status = 1;
        }

        public async void DeactiveItem(string sizeModelItemId)
        {
            var table = await GetSizeModelItem(sizeModelItemId);

            table.Status = 2;
        }

        public async void Delete(string sizeModelId)
        {
            var table = await GetSizeModel(sizeModelId);

            context.SizeModels.Remove(table);
        }

        public async void DeleteItem(string sizeModelItemId)
        {
            var table = await GetSizeModelItem(sizeModelItemId);

            context.SizeModelItems.Remove(table);
        }

        public async Task<SizeModel> Get(string sizeModelId)
        {
            var data = await context.SizeModels.FirstOrDefaultAsync(x => x.Id == sizeModelId);

            var dto = mapper.Map<SizeModel>(data);

            return dto;
        }

        public async Task<SizeModelItem> GetItem(string sizeModelItemId)
        {
            var data = await context.SizeModelItems
                .Include(x => x.SizeModel)
                .Include(x => x.SizeModelTypeItem)
               .FirstOrDefaultAsync(x => x.Id == sizeModelItemId);

            var entity = mapper.Map<SizeModelItem>(data);

            return entity;
        }

        public async void ChangeSizeModelTypeItem(string sizeModelItemId, string sizeModelItemTypeId)
        {
            var item = await GetSizeModelItem(sizeModelItemId);

            item.SizeModelTypeItemId = sizeModelItemTypeId;
        }

        public async void ChangeFactorSelected(string sizeModelItemId, int factorSelected)
        {
            var item = await GetSizeModelItem(sizeModelItemId);

            item.FactorSelected = factorSelected;
        }

        public async void ChangeQuantity(string sizeModelItemId, int quantity)
        {
            var item = await GetSizeModelItem(sizeModelItemId);

            item.Quantity = quantity;
        }

        public async void ChangeTotalCost(string sizeModelItemId, double totalCost)
        {
            var item = await GetSizeModelItem(sizeModelItemId);

            item.TotalCost = totalCost;
        }

        public async void ChangeIsStandard(string sizeModelItemId, bool isStandard)
        {
            var item = await GetSizeModelItem(sizeModelItemId);

            item.IsStandard = isStandard;
        }
    }
}
