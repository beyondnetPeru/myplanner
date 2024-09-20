namespace MyPlanner.Plannings.Api.Dtos.SizeModelType
{
    public class CreateSizeModelTypeFactorDto
    {
        public string SizeModelId { get; }
        public string Code { get; private set; }
        public string Name { get; private set; }
        public string UserId { get; }

        public CreateSizeModelTypeFactorDto(string sizeModelId, string code, string name, string userId)
        {
            SizeModelId = sizeModelId;
            Code = code;
            Name = name;
            UserId = userId;
        }
    }
}
