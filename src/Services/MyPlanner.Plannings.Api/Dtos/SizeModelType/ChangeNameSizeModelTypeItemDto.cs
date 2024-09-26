namespace MyPlanner.Plannings.Api.Dtos.SizeModelType
{
    public class ChangeNameSizeModelTypeItemDto : AbstractUserDto
    {
        public string Name { get; set; }

        public ChangeNameSizeModelTypeItemDto(string name, string userId) : base(userId)
        {
            Name = name;
        }
    }
}
