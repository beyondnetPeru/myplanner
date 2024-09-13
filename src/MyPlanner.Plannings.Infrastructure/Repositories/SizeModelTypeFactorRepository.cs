using AutoMapper;
using BeyondNet.Ddd.Interfaces;
using Microsoft.EntityFrameworkCore;
using MyPlanner.Plannings.Domain.SizeModels;
using MyPlanner.Plannings.Infrastructure.Database;
using MyPlanner.Plannings.Infrastructure.Database.Tables;

namespace MyPlanner.Plannings.Infrastructure.Repositories
{
    public class SizeModelTypeFactorRepository : ISizeModelTypeFactorRepository
    {
        private readonly PlanningDbContext context;
        private readonly IMapper mapper;

        public IUnitOfWork UnitOfWork => context;

        public SizeModelTypeFactorRepository(PlanningDbContext context, IMapper mapper)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<IEnumerable<SizeModelTypeFactor>> GetAll(string sizeModelTypeId)
        {
            var tables = context.SizeModelTypeFactors.Select(x => x.SizeModelType.Id == sizeModelTypeId);

            var entities = mapper.Map<IEnumerable<SizeModelTypeFactor>>(tables);

            return entities;
        }

        public async Task<SizeModelTypeFactor> GetFactorById(string id)
        {
            var table = await context.SizeModelTypeFactors.FirstOrDefaultAsync(x => x.Id == id);

            if (table == null)
            {
                return null;
            }

            var entity = mapper.Map<SizeModelTypeFactor>(table);

            return entity;
        }

        public async Task<SizeModelTypeFactor> GetFactorByCode(string code)
        {
            var table = await context.SizeModelTypeFactors.FirstOrDefaultAsync(x => x.Code == code);

            if (table == null)
            {
                return null;
            }

            var entity = mapper.Map<SizeModelTypeFactor>(table);

            return entity;
        }

        public async Task Add(SizeModelTypeFactor item)
        {
            var table = mapper.Map<SizeModelTypeFactorTable>(item.GetPropsCopy());

            await context.SizeModelTypeFactors.AddAsync(table);
        }

        public async Task Delete(string id)
        {
            var table = await FindSizeModelTypeFactorByIdAsync(id);

            context.SizeModelTypeFactors.Remove(table);
        }

        private async Task<SizeModelTypeFactorTable> FindSizeModelTypeFactorByIdAsync(string id)
        {
            var table = await context.SizeModelTypeFactors.FirstOrDefaultAsync(x => x.Id == id);

            if (table == null)
            {
                return null;
            }

            return table;
        }

        public async Task Update(SizeModelTypeFactor item)
        {
            var table = await FindSizeModelTypeFactorByIdAsync(item.GetPropsCopy().Id.GetValue());

            if (table == null)
            {
                return;
            }

            var propsCopy = item.GetPropsCopy();

            table.Code = propsCopy.Code.GetValue();
            table.Name = propsCopy.Name.GetValue();
        }

        public async Task Activate(SizeModelTypeFactor item)
        {
            var table = await FindSizeModelTypeFactorByIdAsync(item.GetPropsCopy().Id.GetValue());

            if (table == null)
            {
                return;
            }

            var propsCopy = item.GetPropsCopy();

            table.Status = propsCopy.Status.Id;
        }

        public async Task Deactivate(SizeModelTypeFactor item)
        {
            var table = await FindSizeModelTypeFactorByIdAsync(item.GetPropsCopy().Id.GetValue());

            if (table == null)
            {
                return;
            }

            var propsCopy = item.GetPropsCopy();

            table.Status = propsCopy.Status.Id;
        }
    }
}
