namespace MyPlanner.Plannings.Models.Plan
{
    public class PlanDto
    {
        public string Id { get; set; } = default!;
        public string Name { get; set; } = default!;
        public string Owner { get; set; } = default!;
        public ICollection<PlanCategoryDto> Categories { get; set; } 
        public string SizeModelTypeId { get; set; } = default!;
        public string SizeModelTypeName { get; set; } = default!;
        public List<PlanItemDto> Items { get; set; }
        public string Status { get; set; } = default!;

        public PlanDto()
        {
            Categories = new List<PlanCategoryDto>();
            Items = new List<PlanItemDto>();
        }
    }
}
