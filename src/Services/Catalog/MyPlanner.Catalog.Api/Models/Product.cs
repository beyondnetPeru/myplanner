namespace MyPlanner.Catalog.Api.Models
{
    public class Product
    {
        public string Id { get; set; } = default!;
        public string CompanyId { get; set; } = default!;
        public string Name { get; set; } = default!;
        public List<string> Category { get; set; } = new();
        public string Description { get; set; } = default!;
        public string ImageFile { get; set; } = default!;
        public decimal Price { get; set; } = default!;

        public Product() { 
            Id = Guid.NewGuid().ToString();
        }
    }
}
