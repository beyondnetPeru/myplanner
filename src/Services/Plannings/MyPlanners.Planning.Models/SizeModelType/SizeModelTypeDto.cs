namespace MyPlanner.Plannings.Models.SizeModelType
{
    public class SizeModelTypeDto
    {
        public string Id { get; set; } = default!;
        public string Code { get; set; } = default!;
        public string Name { get; set; } = default!;
        public string Status { get; set; } = default!; 
        public ICollection<SizeModelTypeItemDto> Items { get; set; }

        public SizeModelTypeDto() {
            Items = new List<SizeModelTypeItemDto>();
        }
    }
}
