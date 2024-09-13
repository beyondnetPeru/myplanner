namespace MyPlanner.Plannings.Api.Dtos.SizeModel
{
    public class DeactivateSizeModelItemDto
    {
        public string SizeModelId { get; set; }
        public string SizeModelItemId { get; set; }
        public string UserId { get; }

        public DeactivateSizeModelItemDto(string sizeModelId, string sizeModelItemId, string userId)
        {
            SizeModelId = sizeModelId;
            SizeModelItemId = sizeModelItemId;
            UserId = userId;
        }
    }
}
