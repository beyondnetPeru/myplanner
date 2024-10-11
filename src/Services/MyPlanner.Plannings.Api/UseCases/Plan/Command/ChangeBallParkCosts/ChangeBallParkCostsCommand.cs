using MyPlanner.Shared.Cqrs;
using MyPlanner.Shared.Cqrs.Interfaces;

namespace MyPlanner.Plannings.Api.UseCases.Plan.Command.ChangeBallParkCosts
{
    public class ChangeBallParkCostsCommand : ICommand<ResultSet>
    {
        public string PlanItemId { get; set; }
        public int CurrencySymbol { get; set; } = 1;
        public double BallParkCost { get; private set; } = 0.00;
        public double BallparkDependenciesCost { get; private set; } = 0.00;
        public string UserId { get; set; }

        public ChangeBallParkCostsCommand(string planItemId, int currencySymbol, double ballParkCost, double ballparkDependenciesCost, string userId)
        {
            PlanItemId = planItemId;
            CurrencySymbol = currencySymbol;
            BallParkCost = ballParkCost;
            BallparkDependenciesCost = ballparkDependenciesCost;
            UserId = userId;
        }
    }
}
