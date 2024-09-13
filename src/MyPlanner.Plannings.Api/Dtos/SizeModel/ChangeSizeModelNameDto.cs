namespace MyPlanner.Plannings.Api.Dtos.SizeModel
{
    public class ChangeSizeModelNameDto
    {
        public ChangeSizeModelNameDto(string sizeModelId, string name, string userId)
        {
            SizeModelId = sizeModelId;
            Name = name;
            UserId = userId;
        }

        public string SizeModelId { get; }
        public string Name { get; }
        public string UserId { get; }
    }
}
