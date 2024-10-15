
namespace MyPlanner.Plannings.Models.SizeModel
{
    public class CreateSizeModelDto : AbstractUserDto
    {
        public string SizeModelTypeId { get; set; } = default!;
        public string SizeModelTypeCode { get; set; } = default!;
        public string Name { get; set; } = default!;
        public ICollection<CreateSizeModelItemDto> Items { get; set; }
        public CreateSizeModelDto(string userId) : base(userId)
        {
            Items = new List<CreateSizeModelItemDto>();
        }
    }
}
