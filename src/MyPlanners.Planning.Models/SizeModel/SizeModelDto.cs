
namespace MyPlanner.Plannings.Models.SizeModel
{
    public class SizeModelDto
    {
        public string Id { get; set; } = default!;
        public string SizeModelTypeId { get; set; } = default!;
        public string Name { get; set; } = default!;
        public ICollection<SizeModelItemDto> Items { get; set; } 
        public string Status { get; set; } = default!;

        public SizeModelDto()
        {
            Items = new List<SizeModelItemDto>();
        }
    }
}
