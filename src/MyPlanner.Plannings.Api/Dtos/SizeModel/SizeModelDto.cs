using MyPlanner.Plannings.Shared.Infrastructure.Database;

namespace MyPlanner.Plannings.Api.Dtos.SizeModel
{
    public class SizeModelDto
    {
        public string Id { get; set; }
        public string Code { get; set; }
        public string SizeModelTypeId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int IsStandard { get; set; }
        public ICollection<SizeModelItemDto> SizeModelItems { get; set; }
        public int Status { get; set; }
    }
}
