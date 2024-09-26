namespace MyPlanner.Shared.Models
{
    public class PaginationRequest
    {
        public int PageSize { get; set; }
        public int PageIndex { get; set; }

        public PaginationRequest()
        {
            PageSize = 10;
            PageIndex = 0;
        }
    }
}
