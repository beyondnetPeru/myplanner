using MyPlanner.Plannings.Domain.PlanAggregate;

namespace MyPlanner.Plannings.Api.Dtos.Plan
{
    public class PlanDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Owner { get; set; }
        public string SizeModelTypeId { get; set; }
        public string SizeModelTypeName { get; set; }
        public string Status { get; set; }
    }
}
