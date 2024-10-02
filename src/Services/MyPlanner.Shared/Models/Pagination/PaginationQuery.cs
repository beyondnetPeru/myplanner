namespace MyPlanner.Shared.Models.Pagination
{
    public class PaginationQuery
    {
        public int Page { get; private set; }
        public int RecordsPerPageMax { get; private set; }

        private int recordsPerPage = 10;

        public PaginationQuery(int page = 1, int recordsPerPageMax = 50)
        {
            Page = page;
            RecordsPerPageMax = recordsPerPageMax;
        }

        public int RecordsPerPage
        {
            get
            {
                return recordsPerPage;
            }
            set
            {
                if (value > RecordsPerPageMax)
                {
                    recordsPerPage = RecordsPerPageMax;
                }
                else
                {
                    recordsPerPage = value;
                }
            }
        }
    }
}

