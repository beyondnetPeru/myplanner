


namespace MyPlanner.Plannings.Api.UseCases.SizeModels.Command.DeactivateSizeModel
{
    public class DeactivateSizeModelCommand : ICommand<ResultSet>
    {
        public DeactivateSizeModelCommand(string sizeModelId, string userId)
        {
            SizeModelId = sizeModelId;
            UserId = userId;
        }

        public string SizeModelId { get; }
        public string UserId { get; }
    }
}
