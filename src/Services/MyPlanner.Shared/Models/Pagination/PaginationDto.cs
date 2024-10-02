namespace MyPlanner.Shared.Models.Pagination
{
    public class PaginationDto
    {
        public int Page { get; private set; } 
        public int RecordsPerPageMax { get; private set; }

        public PaginationDto(int page = 1, int recordsPerPageMax = 50)
        {
            Page = page;
            RecordsPerPageMax = recordsPerPageMax;
        }
    }
}
