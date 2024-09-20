namespace MyPlanner.Plannings.Api.Dtos.SizeModelType
{
    public class SizeModelTypeDto
    {
        public string Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
        public ICollection<SizeModelTypeItemDto> Items { get; set; }
    }
}
