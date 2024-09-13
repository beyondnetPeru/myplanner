namespace MyPlanner.Plannings.Api.Dtos.SizeModelType
{
    public class ChangeNameSizeModelTypeDto
    {
        public ChangeNameSizeModelTypeDto(string sizeModelTypeId, string name, string userId)
        {
            SizeModelTypeId = sizeModelTypeId;
            Name = name;
            UserId = userId;
        }

        public string SizeModelTypeId { get; }
        public string Name { get; }
        public string UserId { get; set; }
    }
}
