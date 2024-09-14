
namespace MyPlanner.Plannings.Api.Dtos.SizeModel
{
    public class CreateSizeModelDto
    {
        public string Code { get; set; }
        public string SizeModelTypeId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int IsStandard { get; set; }
        public string UserId { get; set; }

    }
}
