namespace MyPlanner.Plannings.Models.SizeModel
{
    public class ChangeNameSizeModelDto : AbstractUserDto
    {
        public ChangeNameSizeModelDto(string name, string userId) : base(userId)
        {
            Name = name;
        }

        public string Name { get; }
    }
}
