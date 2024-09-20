namespace MyPlanner.Plannings.Api.Dtos.SizeModelType
{
    public class CreateSizeModelTypeItemDto
    {
        public string Code { get; private set; }
        public string Name { get; private set; }
        public string UserId { get; }

        public CreateSizeModelTypeItemDto(string code, string name, string userId)
        {
            Code = code;
            Name = name;
            UserId = userId;
        }
    }
}
