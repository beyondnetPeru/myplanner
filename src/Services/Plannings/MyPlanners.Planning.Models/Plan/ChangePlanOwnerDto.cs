namespace MyPlanner.Plannings.Models.Plan
{
    public class ChangePlanOwnerDto : AbstractUserDto
    {
        public string Id { get; set; }
        public string Owner { get; set; }

        public ChangePlanOwnerDto(string id, string owner, string userId) : base(userId)
        {
            Id = id;
            Owner = owner;
        }
    }
}
