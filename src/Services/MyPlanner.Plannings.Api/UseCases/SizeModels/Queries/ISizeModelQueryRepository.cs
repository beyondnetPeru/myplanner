using MyPlanner.Plannings.Api.Dtos.SizeModel;
using MyPlanner.Plannings.Shared.Application.Dtos;

namespace MyPlanner.Plannings.Api.UseCases.SizeModels.Queries
{
    public interface ISizeModelQueryRepository
    {
        Task<IEnumerable<SizeModelDto>> GetAll(PaginationDto pagination);
        Task<SizeModelDto> Get(string sizeModelId);
        Task<IEnumerable<SizeModelItemDto>> GetItems(string sizeModelId);
        Task<SizeModelItemDto> GetItem(string sizeModelItemId);
    }
}
