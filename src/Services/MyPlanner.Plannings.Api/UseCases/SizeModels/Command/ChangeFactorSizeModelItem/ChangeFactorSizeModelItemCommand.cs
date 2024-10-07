using MyPlanner.Shared.Cqrs;
using MyPlanner.Shared.Cqrs.Interfaces;

namespace MyPlanner.Plannings.Api.UseCases.SizeModels.Command.ChangeFactorSizeModel
{
    public class ChangeFactorSizeModelItemCommand : ICommand<ResultSet>
    {
        public string SizeModelItemId { get; set; }
        public int FactorSelected { get; set; }
        public string UserId { get; set; }

        public ChangeFactorSizeModelItemCommand(string sizeModelItemId, int factorSelected, string userId)
        {
            SizeModelItemId = sizeModelItemId;
            FactorSelected = factorSelected;
            UserId = userId;
        }
    }
}
