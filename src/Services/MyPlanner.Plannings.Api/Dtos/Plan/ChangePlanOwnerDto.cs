namespace MyPlanner.Plannings.Api.Dtos.Plan
{
    public class ChangePlanOwnerDto
    {
        public string Id { get; set; }
        public string Owner { get; set; }
        public string UserId { get; }

        public ChangePlanOwnerDto(string id, string owner, string userId)
        {
            Id = id;
            Owner = owner;
            UserId = userId;
        }
    }
}
