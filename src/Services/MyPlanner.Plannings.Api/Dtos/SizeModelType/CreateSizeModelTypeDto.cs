using MyPlanner.Shared.Infrastructure.Database;

namespace MyPlanner.Plannings.Api.Dtos.SizeModelType
{
    public class CreateSizeModelTypeDto : AbstractUserDto
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<CreateSizeModelTypeItemDto> Items { get; set; } = new List<CreateSizeModelTypeItemDto>();

        public CreateSizeModelTypeDto(string code, string name, string description, string userId) : base(userId)
        {
            Code = code;
            Name = name;
            Description = description;
        }
    }
}
