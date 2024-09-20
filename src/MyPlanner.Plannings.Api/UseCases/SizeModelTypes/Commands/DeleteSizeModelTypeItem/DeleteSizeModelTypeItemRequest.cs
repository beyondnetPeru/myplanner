namespace MyPlanner.Plannings.Api.UseCases.SizeModelTypes.Commands.DeleteSizeModelTypeItem
{
    public class DeleteSizeModelTypeItemRequest : IRequest<bool>
    {
        public string SizeModelTypeItemId { get; set; }
        public string UserId { get; set; }

        public DeleteSizeModelTypeItemRequest(string sizeModelTypeItemId, string userId)
        {
            SizeModelTypeItemId = sizeModelTypeItemId;
            UserId = userId;
        }

    }
}
