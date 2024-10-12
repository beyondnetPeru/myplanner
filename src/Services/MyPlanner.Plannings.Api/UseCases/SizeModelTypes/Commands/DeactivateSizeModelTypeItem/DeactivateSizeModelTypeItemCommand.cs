


namespace MyPlanner.Plannings.Api.UseCases.SizeModelTypes.Commands.DeactivateSizeModelTypeFactor
{
    public class DeactivateSizeModelTypeItemCommand : ICommand<ResultSet>
    {
        public DeactivateSizeModelTypeItemCommand(string sizeModelTypeId, string sizeModelTypeItemId, string userId)
        {
            SizeModelTypeId = sizeModelTypeId;
            SizeModelTypeItemId = sizeModelTypeItemId;
            UserId = userId;
        }

        public string SizeModelTypeId { get; }
        public string SizeModelTypeItemId { get; }
        public string UserId { get; }
    }
}
