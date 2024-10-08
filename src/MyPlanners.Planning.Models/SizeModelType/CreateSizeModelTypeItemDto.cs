namespace MyPlanner.Plannings.Models.SizeModelType
{
    public class CreateSizeModelTypeItemDto : AbstractUserDto
    {
        public string Code { get; private set; }
        public string Name { get; private set; }

        public CreateSizeModelTypeItemDto(string code, string name, string userId) : base(userId)
        {
            Code = code;
            Name = name;
        }
    }
}
