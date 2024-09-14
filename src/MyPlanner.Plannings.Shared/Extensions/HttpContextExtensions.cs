using Microsoft.AspNetCore.Http;

namespace MyPlanner.Plannings.Shared.Extensions
{
    public static class HttpContextExtensions
    {
        public async static Task AddPaginationInHeader<T>(this HttpContext httpContext, IQueryable<T> queryable, int page = 0, int recordsPerPage = 10)
        {
            if (httpContext == null)
            {
                throw new ArgumentNullException(nameof(httpContext));
            }

            double count = queryable.Count();
            double totalPage = Math.Ceiling(count / recordsPerPage);

            httpContext.Response.Headers.Append("X-Pagination-TotalCountRecords", count.ToString());
            httpContext.Response.Headers.Append("X-Pagination-TotalPages", totalPage.ToString());
            httpContext.Response.Headers.Append("X-Pagination-CurrentPage", page.ToString());
            httpContext.Response.Headers.Append("X-Pagination-RecordsPerPage", recordsPerPage.ToString());

            await Task.CompletedTask;
        }
    }
}
