using MyPlanner.Plannings.Shared.Infrastructure.Database;

namespace MyPlanner.Plannings.Api.Dtos.SizeModel
{
    public class CreateSizeModelDto
    {
        public string Code { get; set; }
        public string SizeModelTypeCode { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int IsStandard { get; set; }

        public string UserId { get; set; }
        public ICollection<SizeModelItemDto> SizeModelItems { get; set; }
    }
}
