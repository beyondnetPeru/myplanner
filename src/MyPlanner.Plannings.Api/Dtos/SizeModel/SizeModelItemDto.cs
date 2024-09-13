using MyPlanner.Plannings.Shared.Infrastructure.Database;

namespace MyPlanner.Plannings.Api.Dtos.SizeModel
{
    public class SizeModelItemDto
    {
        public string SizeModelId { get; set; }
        public string ProfileName { get; set; }
        public double ProfileAvgRateAmount { get; set; }
        public string SizeModelTypeFactorCode { get; set; }
        public string ProfileValueSelected { get; set; }
        public int ProfileQuantity { get; set; }
        public double TotalCost { get; set; }
        public string UserId { get; }
    }
}
