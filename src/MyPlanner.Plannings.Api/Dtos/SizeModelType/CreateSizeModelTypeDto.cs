using MyPlanner.Plannings.Shared.Infrastructure.Database;

namespace MyPlanner.Plannings.Api.Dtos.SizeModelType
{
    public class CreateSizeModelTypeDto
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<AddSizeModelTypeFactorDto> Factors { get; set; } = new List<AddSizeModelTypeFactorDto>();
        public string UserId { get; set; }
    }
}
