namespace MyPlanner.Plannings.Models.SizeModel
{
    public class ChangeTotalCostSizeModelItemDto : AbstractUserDto
    {
        public double TotalCost { get; set; }

        public ChangeTotalCostSizeModelItemDto(double totalCost, string userId) : base(userId)
        {
            TotalCost = totalCost;
            UserId = userId;
        }
    }
}
