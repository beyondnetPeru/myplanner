using MyPlanner.Plannings.Api.Dtos.SizeModel;

namespace MyPlanner.Plannings.Api.UseCases.SizeModels.Queries
{
    public interface ISizeModelQueryRepository
    {
        Task<IEnumerable<SizeModelDto>> GetAll(PaginationQuery pagination);
        Task<SizeModelDto> Get(string sizeModelId);
        Task<IEnumerable<SizeModelItemDto>> GetItems(string sizeModelId);
        Task<SizeModelItemDto> GetItem(string sizeModelItemId);
    }
}
