namespace MyPlanner.Catalog.Api.Models
{
    public class Company
    {
        public string Id { get; set; } = default!;
        public string Name { get; set; } = default!;
        public Company()
        {
            Id = Guid.NewGuid().ToString();
        }
    }
}
