namespace MyPlanner.Plannings.Api.Dtos.SizeModelType
{
    public class DeactivateSizeModelTypeDto
    {
        public string SizeModelTypeId { get; set; }
        public string UserId { get; set; }

        public DeactivateSizeModelTypeDto(string sizeModelTypeId, string userId)
        {
            SizeModelTypeId = sizeModelTypeId;
            UserId = userId;
        }

    }
}
