namespace MyPlanner.Shared.Extensions
{
    public static class HttpContextExtensions
    {
        /// <summary>
        /// Adds pagination information to the response headers.
        /// </summary>
        /// <typeparam name="T">The type of the queryable.</typeparam>
        /// <param name="httpContext">The HttpContext.</param>
        /// <param name="queryable">The queryable data.</param>
        /// <param name="page">The current page number.</param>
        /// <param name="recordsPerPage">The number of records per page.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public static async Task AddPaginationInHeader<T>(this HttpContext httpContext, IQueryable<T> queryable, int page = 0, int recordsPerPage = 10)
        {
            if (httpContext == null)
            {
                throw new ArgumentNullException(nameof(httpContext));
            }

            double count = await queryable.CountAsync();
            double totalPage = Math.Ceiling(count / recordsPerPage);

            httpContext.Response.Headers.Append("X-Pagination-TotalCountRecords", count.ToString());
            httpContext.Response.Headers.Append("X-Pagination-TotalPages", totalPage.ToString());
            httpContext.Response.Headers.Append("X-Pagination-CurrentPage", page.ToString());
            httpContext.Response.Headers.Append("X-Pagination-RecordsPerPage", recordsPerPage.ToString());

            await Task.CompletedTask;
        }
    }
}
