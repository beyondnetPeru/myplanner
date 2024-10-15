namespace MyPlanner.Shared.Models.Pagination
{
    

    public class PaginationQuery
    {
        public int Page { get; private set; }
        public int RecordsPerPage { get; private set; }

        public int Skip { get; private set; }
        public int Take { get; private set; }

        public PaginationQuery(int page = 1, int recordsPerPage = 5)
        {
            this.Skip = (page - 1) * recordsPerPage;
            this.Take = recordsPerPage;
        }

     }
}

