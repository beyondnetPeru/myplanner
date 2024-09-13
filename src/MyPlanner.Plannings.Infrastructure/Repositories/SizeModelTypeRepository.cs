using AutoMapper;
using BeyondNet.Ddd.Interfaces;
using Microsoft.EntityFrameworkCore;
using MyPlanner.Plannings.Api.Endpoints;
using MyPlanner.Plannings.Domain.SizeModels;
using MyPlanner.Plannings.Infrastructure.Database;
using MyPlanner.Plannings.Infrastructure.Database.Tables;
using MyPlanner.Plannings.Shared.Application.Dtos;

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


        public async Task<IEnumerable<SizeModelType>> GetAll(PaginationDto pagination)
        {
            var data = await context.SizeModelTypes
            .OrderBy(c => c.Name)
            .Skip(pagination.RecordsPerPage * pagination.Page)
            .Take(pagination.RecordsPerPage)
            .ToListAsync();

            var entities = mapper.Map<ICollection<SizeModelType>>(data);

            return entities;
        }

        public async Task<SizeModelType> GetByCode(string code)
        {
            var data = await context.SizeModelTypes.FirstOrDefaultAsync(x => x.Code == code);

            var entity = mapper.Map<SizeModelType>(data);

            return entity;
        }

        public async Task<SizeModelType> GetById(string id)
        {
            var data = await context.SizeModelTypes.FirstOrDefaultAsync(x => x.Id == id);

            var entity = mapper.Map<SizeModelType>(data);

            return entity;
        }

        public async Task Add(SizeModelType item)
        {
            var propsCopy = item.GetPropsCopy();

            var table = mapper.Map<SizeModelTypeTable>(propsCopy);

            await context.SizeModelTypes.AddAsync(table);
        }

        private async Task<SizeModelTypeTable> FindSizeModelTypeByIdAsync(string id)
        {
            var table = await context.SizeModelTypes.FirstOrDefaultAsync(x => x.Id == id);

            if (table == null)
            {
                throw new KeyNotFoundException($"Entity \"{nameof(SizeModelTypeTable)}\" ({id}) was not found.");
            }

            return table;
        }

        public async Task Delete(string id)
        {
            var table = await FindSizeModelTypeByIdAsync(id);

            context.SizeModelTypes.Remove(table);
        }

        public async Task ChangeCode(string id, string code)
        {
            var table = await FindSizeModelTypeByIdAsync(id);

            if (table == null)
            {
                throw new KeyNotFoundException($"Entity \"{nameof(SizeModelTypeTable)}\" ({id}) was not found.");
            }

            table.Code = code;
        }

        public async Task ChangeName(string id, string name)
        {
            var table = await FindSizeModelTypeByIdAsync(id);

            if (table == null)
            {
                throw new KeyNotFoundException($"Entity \"{nameof(SizeModelTypeTable)}\" ({id}) was not found.");
            }

            table.Name = name;
        }

        public async Task Activate(string id)
        {
            var table = await FindSizeModelTypeByIdAsync(id);

            if (table == null)
            {
                throw new KeyNotFoundException($"Entity \"{nameof(SizeModelTypeTable)}\" ({id}) was not found.");
            }

            table.Status = 1;
        }

        public async Task Deactivate(string id)
        {
            var table = await FindSizeModelTypeByIdAsync(id);

            if (table == null)
            {
                throw new KeyNotFoundException($"Entity \"{nameof(SizeModelTypeTable)}\" ({id}) was not found.");
            }

            table.Status = 0;
        }
    }
}
