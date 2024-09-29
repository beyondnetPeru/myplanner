namespace MyPlanner.Shared.Infrastructure.Marten.Pagination
{
    public record GetPaginationRequest(int? PageNumber, int? PageSize);
}
