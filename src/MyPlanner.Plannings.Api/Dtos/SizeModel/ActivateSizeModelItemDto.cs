namespace MyPlanner.Plannings.Api.Dtos.SizeModel
{
    public class ActivateSizeModelItemDto
    {
        public string SizeModelId { get; set; }
        public string SizeModelItemId { get; set; }
        public string UserId { get; }

        public ActivateSizeModelItemDto(string sizeModelId, string sizeModelItemId, string userId)
        {
            SizeModelId = sizeModelId;
            SizeModelItemId = sizeModelItemId;
            UserId = userId;
        }
    }
}
