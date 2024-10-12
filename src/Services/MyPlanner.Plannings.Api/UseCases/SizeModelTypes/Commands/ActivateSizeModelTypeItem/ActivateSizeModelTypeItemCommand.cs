


namespace MyPlanner.Plannings.Api.UseCases.SizeModelTypes.Commands.ActivateSizeModelTypeFactor
{
    public class ActivateSizeModelTypeItemCommand : ICommand<ResultSet>
    {
        public ActivateSizeModelTypeItemCommand(string sizeModelTypeId, string sizeModelTypeItemId, string userId)
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
