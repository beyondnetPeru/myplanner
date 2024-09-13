namespace MyPlanner.Plannings.Api.Dtos.Plan
{
    public class CreatePlanDto
    {
        public string Name { get; set; }
        public string Owner { get; set; }
        public string SizeModelTypeId { get; set; }
        public string UserId { get; set; }
    }
}
