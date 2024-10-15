


namespace MyPlanner.Plannings.Api.UseCases.SizeModelTypes.Commands.ChangeNameSizeModelType
{
    public class ChangeNameSizeModelTypeCommand : ICommand<ResultSet>
    {
        public ChangeNameSizeModelTypeCommand(string sizeModelTypeId, string name, string userId)
        {
            SizeModelTypeId = sizeModelTypeId;
            Name = name;
            UserId = userId;
        }

        public string SizeModelTypeId { get; }
        public string Name { get; }
        public string UserId { get; }
    }
}
