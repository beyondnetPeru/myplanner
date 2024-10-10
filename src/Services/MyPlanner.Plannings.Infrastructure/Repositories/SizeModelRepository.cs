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


        public async Task<ICollection<SizeModel>> GetAll()
        {
            var data = await context.SizeModels.ToListAsync();

            var dto = mapper.Map<ICollection<SizeModel>>(data);

            return dto;
        }

        public async Task<SizeModel> Get(string sizeModelId)
        {
            var data = await context.SizeModels.FirstOrDefaultAsync(x => x.Id == sizeModelId);

            var dto = mapper.Map<SizeModel>(data);

            return dto;
        }

        public async void Update(SizeModel sizeModel)
        {
            var table = await GetSizeModel(sizeModel.GetPropsCopy().Id.GetValue());

            table.SizeModelTypeId = sizeModel.GetPropsCopy().SizeModelTypeId.GetValue();
            table.SizeModelTypeCode = sizeModel.GetPropsCopy().SizeModelTypeCode.GetValue();
            table.Name = sizeModel.GetPropsCopy().Name.GetValue();
            table.Status = sizeModel.GetPropsCopy().Status.Id;
            table.Audit.CreatedBy = sizeModel.GetPropsCopy().Audit.GetValue().CreatedBy;
            table.Audit.CreatedAt = sizeModel.GetPropsCopy().Audit.GetValue().CreatedAt;
            table.Audit.UpdatedBy = sizeModel.GetPropsCopy().Audit.GetValue().UpdatedBy;
            table.Audit.UpdatedAt = sizeModel.GetPropsCopy().Audit.GetValue().UpdatedAt;
            table.Audit.TimeSpan = sizeModel.GetPropsCopy().Audit.GetValue().TimeSpan;

        }
        public void Add(SizeModel sizeModel)
        {
            var sizeModelProps = sizeModel.GetPropsCopy();

            var table = mapper.Map<SizeModelTable>(sizeModelProps);

            context.SizeModels.Add(table);
        }

        public async void Delete(string sizeModelId)
        {
            var table = await GetSizeModel(sizeModelId);

            table.Status = 0;
        }

        public async Task<ICollection<SizeModelItem>> GetAllItem(string sizeModelId)
        {
            var data = await context.SizeModelItems
                .Include(x => x.SizeModel)
                .Where(x => x.SizeModelId == sizeModelId)
                .ToListAsync();

            var entity = mapper.Map<ICollection<SizeModelItem>>(data);

            return entity;
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

        public void AddItem(SizeModelItem sizeModelItem)
        {
            var table = mapper.Map<SizeModelItemTable>(sizeModelItem);

            context.SizeModelItems.Add(table);
        }      



        public async void UpdateItem(SizeModelItem sizeModelItem)
        {
            var table = await GetSizeModelItem(sizeModelItem.GetPropsCopy().Id.GetValue());

            table.SizeModelId = sizeModelItem.GetPropsCopy().SizeModelId.GetValue();
            table.SizeModelTypeItemId = sizeModelItem.GetPropsCopy().SizeModelTypeItemId.GetValue();
            table.SizeModelTypeItemCode = sizeModelItem.GetPropsCopy().SizeModelTypeItemCode.GetValue();
            table.FactorSelected = sizeModelItem.GetPropsCopy().FactorSelected.Id;
            table.ProfileName = sizeModelItem.GetPropsCopy().Profile.GetValue().ProfileName.GetValue();
            table.ProfileAvgRateSymbol = sizeModelItem.GetPropsCopy().Profile.GetValue().ProfileAvgRate.GetValue().Symbol.Id;
            table.ProfileAvgRateValue = sizeModelItem.GetPropsCopy().Profile.GetValue().ProfileAvgRate.GetValue().Value;
            table.Quantity = sizeModelItem.GetPropsCopy().Quantity.GetValue();
            table.TotalCost = sizeModelItem.GetPropsCopy().TotalCost.GetValue();
            table.IsStandard = sizeModelItem.GetPropsCopy().IsStandard.GetValue();
            table.Status = sizeModelItem.GetPropsCopy().Status.Id;
            table.Audit.CreatedBy = sizeModelItem.GetPropsCopy().Audit.GetValue().CreatedBy;
            table.Audit.CreatedAt = sizeModelItem.GetPropsCopy().Audit.GetValue().CreatedAt;
            table.Audit.UpdatedBy = sizeModelItem.GetPropsCopy().Audit.GetValue().UpdatedBy;
            table.Audit.UpdatedAt = sizeModelItem.GetPropsCopy().Audit.GetValue().UpdatedAt;
            table.Audit.TimeSpan= sizeModelItem.GetPropsCopy().Audit.GetValue().TimeSpan;

        }


        private async Task<SizeModelTable> GetSizeModel(string sizeModelId)
        {
            return await context.SizeModels.FirstAsync(x => x.Id == sizeModelId);
        }

        private async Task<SizeModelItemTable> GetSizeModelItem(string sizeModelItemId)
        {
            return await context.SizeModelItems.FirstAsync(x => x.Id == sizeModelItemId);
        }

     
        public async Task DeleteItem(string sizeModelId, string sizeModelItemId)
        {
            var table = await GetSizeModelItem(sizeModelItemId);

            table.Status = 0;
        }
    }
}
