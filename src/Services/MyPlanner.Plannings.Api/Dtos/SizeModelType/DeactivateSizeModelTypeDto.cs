namespace MyPlanner.Plannings.Api.Dtos.SizeModelType
{
    public class DeactivateSizeModelTypeDto : AbstractUserDto
    {
        public DeactivateSizeModelTypeDto(string userId) : base(userId)
        {
            UserId = userId;
        }

    }
}
