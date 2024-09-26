namespace MyPlanner.Plannings.Api.UseCases.SizeModelTypes.Commands.ChangeNameSizeModelTypeItem
{
    public class ChangeNameSizeModelTypeItemRequest : IRequest<bool>
    {
        public string SizeModelTypeItemId { get; set; }
        public string Name { get; set; }

        public string UserId { get; set; }

        public ChangeNameSizeModelTypeItemRequest(string sizeModelTypeItemId, string name, string userId)
        {
            SizeModelTypeItemId = sizeModelTypeItemId;
            Name = name;
            UserId = userId;
        }
    }
}
