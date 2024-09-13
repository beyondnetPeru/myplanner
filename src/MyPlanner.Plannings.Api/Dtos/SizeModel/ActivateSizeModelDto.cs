namespace MyPlanner.Plannings.Api.Dtos.SizeModel
{
    public class ActivateSizeModelDto
    {
        public string SizeModelId { get; set; }
        public string UserId { get; set; }

        public ActivateSizeModelDto(string sizeModelId, string userId)
        {
            SizeModelId = sizeModelId;
            UserId = userId;
        }
    }
}
