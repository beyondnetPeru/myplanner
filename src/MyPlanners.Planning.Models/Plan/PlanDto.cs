namespace MyPlanner.Plannings.Models.Plan
{
    public class PlanDto
    {
        public string Id { get; set; } = default!;
        public string Name { get; set; } = default!;
        public string Owner { get; set; } = default!;
        public string SizeModelTypeId { get; set; } = default!;
        public string SizeModelTypeName { get; set; } = default!;   
        public string Status { get; set; } = default!;
    }
}
