


namespace MyPlanner.Plannings.Api.UseCases.SizeModelTypes.Commands.ChangeNameSizeModelTypeItem
{
    public class ChangeNameSizeModelTypeItemCommand : ICommand<ResultSet>
    {
        public string SizeModelTypeId { get; }
        public string SizeModelTypeItemId { get; set; }
        public string Name { get; set; }

        public string UserId { get; set; }

        public ChangeNameSizeModelTypeItemCommand(string sizeModelTypeId, string sizeModelTypeItemId, string name, string userId)
        {
            SizeModelTypeId = sizeModelTypeId;
            SizeModelTypeItemId = sizeModelTypeItemId;
            Name = name;
            UserId = userId;
        }
    }
}
