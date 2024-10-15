namespace MyPlanner.Plannings.Infrastructure.Database.Tables
{
    public class PlanCategoryTable
    {
        [Key]
        public string Id { get; set; }
        [ForeignKey("PlanId")]
        public string PlanId { get; set; }
        public PlanTable Plan { get; set; }
        public string Name { get; set; }
    }
}
