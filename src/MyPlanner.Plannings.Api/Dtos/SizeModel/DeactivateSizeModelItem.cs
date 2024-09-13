namespace MyPlanner.Plannings.Api.Dtos.SizeModel
{
    public class DeactivateSizeModelItem
    {
        public string SizeModelItemId { get; set; }
        public string UserId { get; set; }

        public DeactivateSizeModelItem(string sizeModelItemId, string userId)
        {
            SizeModelItemId = sizeModelItemId;
            UserId = userId;
        }
    }
}
