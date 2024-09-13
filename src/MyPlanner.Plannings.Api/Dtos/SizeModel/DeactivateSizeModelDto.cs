namespace MyPlanner.Plannings.Api.Dtos.SizeModel
{
    public class DeactivateSizeModelDto
    {
        public string SizeModelId { get; set; }
        public string UserId { get; set; }

        public DeactivateSizeModelDto(string sizeModelId, string userId)
        {
            SizeModelId = sizeModelId;
            UserId = userId;
        }
    }
}
