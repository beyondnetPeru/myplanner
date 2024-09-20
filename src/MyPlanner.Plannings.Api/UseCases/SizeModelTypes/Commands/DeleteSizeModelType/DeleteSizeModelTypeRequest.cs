namespace MyPlanner.Plannings.Api.UseCases.SizeModelTypes.Commands.DeleteSizeModelType
{
    public class DeleteSizeModelTypeRequest : IRequest<bool>
    {
        public DeleteSizeModelTypeRequest(string sizeModelTypeId, string userId)
        {
            if (string.IsNullOrEmpty(sizeModelTypeId))
            {
                throw new ArgumentException($"'{nameof(sizeModelTypeId)}' cannot be null or empty.", nameof(sizeModelTypeId));
            }

            if (string.IsNullOrEmpty(userId))
            {
                throw new ArgumentException($"'{nameof(userId)}' cannot be null or empty.", nameof(userId));
            }
            SizeModelTypeId = sizeModelTypeId;
            UserId = userId;
        }

        public string SizeModelTypeId { get; }
        public string UserId { get; }
    }
}
