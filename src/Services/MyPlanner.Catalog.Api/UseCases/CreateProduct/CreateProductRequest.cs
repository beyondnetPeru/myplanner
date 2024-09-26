using MediatR;

namespace MyPlanner.Catalog.Api.UseCases.CreateProduct
{
    public class CreateProductRequest : IRequest<bool>
    {
        public string Name { get; set; } = default!;
        public List<string> Category { get; set; } = new();
        public string Description { get; set; } = default!;
        public string ImageFile { get; set; } = default!;
        public decimal Price { get; set; } = default!;

        public CreateProductRequest(string name,
                                           List<string> category,
                                           string description,
                                           string imageFile,
                                           decimal price)
        {
            Name = name;
            Category = category;
            Description = description;
            ImageFile = imageFile;
            Price = price;
        }


    }
}
