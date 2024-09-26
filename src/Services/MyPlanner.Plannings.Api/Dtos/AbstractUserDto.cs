namespace MyPlanner.Plannings.Api.Dtos
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
