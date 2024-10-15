


namespace MyPlanner.Plannings.Api.UseCases.SizeModelTypes.Commands.DeleteSizeModelType
{
    public class DeleteSizeModelTypeCommand : ICommand<ResultSet>
    {
        public DeleteSizeModelTypeCommand(string sizeModelTypeId, string userId)
        {
            SizeModelTypeId = sizeModelTypeId;
            UserId = userId;
        }

        public string SizeModelTypeId { get; }
        public string UserId { get; }
    }
}
