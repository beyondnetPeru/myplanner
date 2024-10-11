namespace MyPlanner.Planning.Models.Plan
{
    public class ChangeAssumptionsDto : AbstractUserDto
    {
        public ChangeAssumptionsDto(string assumptions, string userId) : base(userId)
        {
            Assumptions = assumptions;
        }

        public string Assumptions { get; set; }
    }
}
