﻿using MyPlanner.Plannings.Api.Dtos.SizeModelType;
using MyPlanner.Plannings.Shared.Application.Dtos;

namespace MyPlanner.Plannings.Api.UseCases.SizeModelTypes.Queries
{
    public interface ISizeModelTypeQueryRepository
    {
        Task<IEnumerable<SizeModelTypeDto>> GetAll(PaginationDto pagination);
        Task<SizeModelTypeDto> GetById(string id);
        Task<SizeModelTypeDto> GetByCode(string code);

        Task<IEnumerable<SizeModelTypeFactorDto>> GetFactors(string sizeModelTypeId);
        Task<SizeModelTypeFactorDto> GetFactorById(string id);
        Task<SizeModelTypeFactorDto> GetFactorByCode(string code);
    }
}