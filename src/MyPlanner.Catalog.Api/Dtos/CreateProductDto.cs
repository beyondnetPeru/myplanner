namespace MyPlanner.Catalog.Api.Dtos
{
    public record CreateProductDto
    {
        public string Name { get; set; } = default!;
        public List<string> Category { get; set; } = new();
        public string Description { get; set; } = default!;
        public string ImageFile { get; set; } = default!;
        public decimal Price { get; set; } = default!;

        public CreateProductDto(string id,
                                string name,
                                List<string> category,
                                string desciption,
                                string imageFile,
                                decimal price)
        {
            Name = name;
            Category = category;
            Description = desciption;
            ImageFile = imageFile;
            Price = price;

        }
    }
}
