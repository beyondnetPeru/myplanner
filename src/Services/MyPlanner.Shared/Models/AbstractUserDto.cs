namespace MyPlanner.Shared.Models
{
    public class AbstractUserDto
    {
        public string UserId { get; set; }

        public AbstractUserDto(string userId)
        {
            UserId = userId;
        }
    }
}
