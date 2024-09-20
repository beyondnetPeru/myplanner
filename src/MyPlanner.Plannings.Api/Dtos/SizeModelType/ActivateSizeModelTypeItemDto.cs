namespace MyPlanner.Plannings.Api.Dtos.SizeModelType
{
    public class ActivateSizeModelTypeItemDto
    {
        public string SizeModelTypeId { get; set; }
        public string SizeModelTypeFactorId { get; set; }
        public string UserId { get; set; }

        public ActivateSizeModelTypeItemDto(string sizeModelTypeId, string sizeModelTypeFactorId, string userId)
        {
            SizeModelTypeId = sizeModelTypeId;
            SizeModelTypeFactorId = sizeModelTypeFactorId;
            UserId = userId;
        }
    }
}
