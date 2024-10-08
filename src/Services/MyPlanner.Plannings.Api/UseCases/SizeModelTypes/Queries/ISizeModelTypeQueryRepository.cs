

namespace MyPlanner.Plannings.Api.UseCases.SizeModelTypes.Queries
{
    public interface ISizeModelTypeQueryRepository
    {
        Task<IEnumerable<SizeModelTypeDto>> GetAll(PaginationQuery pagination);
        Task<SizeModelTypeDto> GetById(string id);
        Task<SizeModelTypeDto> GetByCode(string code);

        Task<IEnumerable<SizeModelTypeItemDto>> GetItems(string sizeModelTypeId);
        Task<SizeModelTypeItemDto> GetItemById(string id);
        Task<SizeModelTypeItemDto> GetItemByCode(string code);
    }
}
