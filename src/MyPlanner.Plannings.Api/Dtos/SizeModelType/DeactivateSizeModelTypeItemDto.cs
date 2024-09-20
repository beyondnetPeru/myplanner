namespace MyPlanner.Plannings.Api.Dtos.SizeModelType
{
    public class DeactivateSizeModelTypeItemDto
    {
        public string SizeModelTypeId { get; set; }
        public string SizeModelTypeFactorId { get; set; }
        public string UserId { get; set; }

        public DeactivateSizeModelTypeItemDto(string sizeModelTypeId, string sizeModelTypeFactorId, string userId)
        {
            SizeModelTypeId = sizeModelTypeId;
            SizeModelTypeFactorId = sizeModelTypeFactorId;
            UserId = userId;
        }
    }
}
