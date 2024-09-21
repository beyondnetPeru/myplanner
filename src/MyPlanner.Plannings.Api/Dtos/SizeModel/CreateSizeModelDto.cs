
namespace MyPlanner.Plannings.Api.Dtos.SizeModel
{
    public class CreateSizeModelDto
    {
        public string SizeModelTypeId { get; set; }
        public string Name { get; set; }
        public ICollection<CreateSizeModelItemDto> Items { get; set; }
        public bool IsStandard { get; set; }
        public string UserId { get; set; }

    }
}
