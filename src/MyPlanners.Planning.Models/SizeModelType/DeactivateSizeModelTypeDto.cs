namespace MyPlanner.Plannings.Models.SizeModelType
{
    public class DeactivateSizeModelTypeDto : AbstractUserDto
    {
        public DeactivateSizeModelTypeDto(string userId) : base(userId)
        {
            UserId = userId;
        }

    }
}
