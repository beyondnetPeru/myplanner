namespace MyPlanner.Plannings.Api.UseCases.SizeModels.Command.ChangeIsStandardSizeModelItem
{
    public class ChangeIsStandardSizeModelItemRequest : IRequest<bool>
    {
        public string SizeModelItemId { get; set; }
        public bool IsStandard { get; set; }

        public string UserId { get; set; }

        public ChangeIsStandardSizeModelItemRequest(string sizeModelItemId, bool isStandard, string userId)
        {
            SizeModelItemId = sizeModelItemId;
            IsStandard = isStandard;
            UserId = userId;
        }
    }
}
