namespace MyPlanner.Plannings.Infrastructure.Database.Tables
{
    public class ErrorTable
    {
        public Guid Id { get; set; }
        public string ErrorMessage { get; set; } = null!;
        public string? StackTrace { get; set; }
        public DateTime Date { get; set; }
    }
}
