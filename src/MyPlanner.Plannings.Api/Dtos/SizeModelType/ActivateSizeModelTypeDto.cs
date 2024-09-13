namespace MyPlanner.Plannings.Api.Dtos.SizeModelType
{
    public class ActivateSizeModelTypeDto
    {
        public string SizeModelTypeId { get; set; }
        public string UserId { get; set; }

        public ActivateSizeModelTypeDto(string sizeModelTypeId, string userId)
        {
            SizeModelTypeId = sizeModelTypeId;
            UserId = userId;
        }
    }
}
