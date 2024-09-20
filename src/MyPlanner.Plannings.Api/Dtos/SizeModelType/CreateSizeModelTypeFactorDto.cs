namespace MyPlanner.Plannings.Api.Dtos.SizeModelType
{
    public class CreateSizeModelTypeFactorDto
    {
        public string Code { get; private set; }
        public string Name { get; private set; }
        public string UserId { get; }

        public CreateSizeModelTypeFactorDto(string code, string name, string userId)
        {
            Code = code;
            Name = name;
            UserId = userId;
        }
    }
}
