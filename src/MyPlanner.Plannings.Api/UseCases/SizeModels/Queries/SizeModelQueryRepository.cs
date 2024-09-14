﻿using Microsoft.EntityFrameworkCore;
using MyPlanner.Plannings.Api.Dtos.SizeModel;
using MyPlanner.Plannings.Infrastructure.Database;
using MyPlanner.Plannings.Shared.Application.Dtos;

namespace MyPlanner.Plannings.Api.UseCases.SizeModels.Queries
{
    public class SizeModelQueryRepository : ISizeModelQueryRepository
    {
        private readonly PlanningDbContext context;
        private readonly IMapper mapper;

        public SizeModelQueryRepository(PlanningDbContext context, IMapper mapper)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<SizeModelDto> Get(string sizeModelId)
        {
            var data = await context.SizeModels.FindAsync(sizeModelId);

            var dto = mapper.Map<SizeModelDto>(data);

            return dto;
        }

        public async Task<IEnumerable<SizeModelDto>> GetAll(PaginationDto pagination)
        {
            var data = await context.SizeModels
                .Skip(pagination.Page)
                .Take(pagination.RecordsPerPage)
                .ToListAsync();

            var dto = mapper.Map<IEnumerable<SizeModelDto>>(data);

            return dto;
        }

        public async Task<SizeModelItemDto> GetItem(string sizeModelId, string sizeModelItemId)
        {
            var data = await context.SizeModelItems.FindAsync(sizeModelId, sizeModelItemId);

            var dto = mapper.Map<SizeModelItemDto>(data);

            return dto;
        }

        public async Task<IEnumerable<SizeModelItemDto>> GetItems(string sizeModelId)
        {
            var data = await context.SizeModelItems
                .Where(x => x.SizeModelId == sizeModelId)
                .ToListAsync();

            var dto = mapper.Map<IEnumerable<SizeModelItemDto>>(data);

            return dto;
        }
    }
}