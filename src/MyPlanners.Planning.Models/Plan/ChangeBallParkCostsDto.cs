namespace MyPlanner.Planning.Models.Plan
{
    public class ChangeBallParkCostsDto : AbstractUserDto
    {
        public ChangeBallParkCostsDto(string userId) : base(userId)
        {
        }

        public int CurrencySymbol { get; set; } = 1;
        public double BallParkCost { get; private set; } = 0.00;
        public double BallparkDependenciesCost { get; private set; } = 0.00;     
    }
}
