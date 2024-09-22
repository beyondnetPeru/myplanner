
namespace MyPlanner.Plannings.Api.Dtos.SizeModel
{
    public class SizeModelDto
    {
        public string Id { get; set; }
        public string SizeModelTypeId { get; set; }
        public string Name { get; set; }
        public ICollection<SizeModelItemDto> Items { get; set; }
        public string Status { get; set; }
    }
}
