


namespace MyPlanner.Plannings.Api.UseCases.SizeModels.Command.ActivateSizeModel
{
    public class ActivateSizeModelCommand : ICommand<ResultSet>
    {
        public ActivateSizeModelCommand(string sizeModelId, string userId)
        {
            SizeModelId = sizeModelId;
            UserId = userId;
        }

        public string SizeModelId { get; }
        public string UserId { get; }
    }
}
