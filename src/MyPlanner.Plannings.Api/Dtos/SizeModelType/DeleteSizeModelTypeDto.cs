namespace MyPlanner.Plannings.Api.Dtos.SizeModelType
{
    public class DeleteSizeModelTypeDto
    {
        public string SizeModelTypeId { get; set; }
        public string UserId { get; set; }

        public DeleteSizeModelTypeDto(string sizeModelTypeId, string userId)
        {
            SizeModelTypeId = sizeModelTypeId;
            UserId = userId;
        }
    }
}
